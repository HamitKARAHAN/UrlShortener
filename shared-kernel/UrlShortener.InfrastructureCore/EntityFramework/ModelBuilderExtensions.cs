namespace UrlShortener.InfrastructureCore.EntityFramework;

using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using UrlShortener.DomainCore.Identity;

public static class ModelBuilderExtensions
{
    public static void MapStronglyTypedUlid<T, TId>(this ModelBuilder modelBuilder, Expression<Func<T, TId>> expression)
        where T : class 
        where TId : StronglyTypedUlid<TId>
    {
        Guard.Against.Null(modelBuilder);
        modelBuilder
            .Entity<T>()
            .Property(expression)
            .HasConversion(id => id.Value, id => (Activator.CreateInstance(typeof(TId), id) as TId)!);
    }

    public static ModelBuilder UseStringForEnums(this ModelBuilder modelBuilder)
    {
        Guard.Against.Null(modelBuilder);
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (IMutableProperty property in entityType.GetProperties())
            {
                if (property.ClrType.IsEnum)
                {
                    continue;
                }

                Type converterType = typeof(EnumToStringConverter<>).MakeGenericType(property.ClrType);
                ValueConverter converterInstance = (ValueConverter)Activator.CreateInstance(converterType)!;
                property.SetValueConverter(converterInstance);
            }
        }

        return modelBuilder;
    }
}
