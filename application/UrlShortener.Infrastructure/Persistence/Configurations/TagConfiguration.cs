using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.TagDetails;
using UrlShortener.Domain.Tags;
using UrlShortener.DomainCore.Extensions;

namespace UrlShortener.Infrastructure.Persistence.Configurations;

public sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        Guard.Against.Null(builder);

        builder.ToTable(name: nameof(Tag).ToLowerInvariant().ToPlural());
        builder
            .HasOne(t => t.TagDetail)
            .WithOne()
            .HasForeignKey<TagDetail>(td => td.TagId);

        builder.ComplexProperty(x => x.ShortUrl).IsRequired();
        builder.ComplexProperty(x => x.LongUrl).IsRequired();
        builder.ComplexProperty(x => x.Description).IsRequired();
        builder.ComplexProperty(x => x.Ip).IsRequired();
    }
}
