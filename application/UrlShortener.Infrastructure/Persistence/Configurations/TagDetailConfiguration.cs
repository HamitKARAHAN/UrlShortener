using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Extensions;
using UrlShortener.Domain.TagDetails;

namespace UrlShortener.Infrastructure.Persistence.Configurations;

public sealed class TagDetailConfiguration : IEntityTypeConfiguration<TagDetail>
{
    public void Configure(EntityTypeBuilder<TagDetail> builder)
    {
        Guard.Against.Null(builder);

        builder.ToTable(nameof(TagDetail).ToLowerInvariant().ToPlural());
    }
}
