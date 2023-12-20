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
    }
}
