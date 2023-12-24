using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.TagDetails;
using UrlShortener.DomainCore.Extensions;

namespace UrlShortener.Infrastructure.Persistence.Configurations;

public sealed class TagDetailConfiguration : IEntityTypeConfiguration<TagDetail>
{
    public void Configure(EntityTypeBuilder<TagDetail> builder)
    {
        Guard.Against.Null(builder);

        builder.ToTable(nameof(TagDetail).ToLowerInvariant().ToPlural());

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.TagId)
            .IsRequired();

        builder
            .Property(x => x.Id)
            .HasColumnName(nameof(TagDetail.Id).ToLowerInvariant());

        builder
            .Property(x => x.ClickedCount)
            .HasDefaultValue(0)
            .HasColumnName("click_count");

        builder
            .Property(x => x.LastCallTime)
            .HasDefaultValue(null)
            .HasColumnName("last_access_time");

        builder
            .Property(x => x.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");

        builder
            .Property(x => x.DeletedAt)
            .HasDefaultValue(null)
            .HasColumnName("deleted_date");

        builder
            .Property(x => x.ModifiedAt)
            .IsConcurrencyToken(true)
            .HasDefaultValue(null)
            .HasColumnName("last_modify_date");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("creation_date");
    }
}