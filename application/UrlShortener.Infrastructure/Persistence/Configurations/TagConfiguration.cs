// <copyright file="TagConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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

        builder
            .ToTable(name: nameof(Tag).ToLowerInvariant().ToPlural());

        builder
            .HasOne(t => t.TagDetail)
            .WithOne()
            .HasForeignKey<TagDetail>(td => td.TagId);

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName(nameof(Tag.Id).ToLowerInvariant());

        builder
            .ComplexProperty(x => x.ShortCode)
            .IsRequired()
            .Property(x => x.Value)
            .HasColumnName("short_code");

        builder
            .ComplexProperty(x => x.LongUrl)
            .IsRequired();

        builder.ComplexProperty(x => x.LongUrl, b =>
        {
            b.Property(u => u.Scheme)
                .IsRequired()
                .HasColumnName(nameof(LongUrl.Scheme).ToLowerInvariant());
            b.Property(u => u.Host)
                .IsRequired()
                .HasColumnName(nameof(LongUrl.Host).ToLowerInvariant());
        });

        builder
            .ComplexProperty(x => x.Description)
            .IsRequired()
            .Property(x => x.Value)
            .HasColumnName(nameof(Description).ToLowerInvariant());

        builder
            .ComplexProperty(x => x.Ip)
            .IsRequired();

        builder.ComplexProperty(x => x.Ip, b =>
        {
            b.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("ip_address");
            b.Property(p => p.Type)
                .IsRequired()
                .HasColumnName("ip_address_type");
        });

        builder.Property(x => x.IsPublic)
            .HasDefaultValue(false)
            .HasColumnName("is_public");

        builder
            .Property(x => x.IsDeleted)
            .HasDefaultValue(false)
            .HasColumnName("is_deleted");

        builder.Ignore(x => x.DomainEvents);

        builder
            .Property(x => x.ModifiedAt)
            .IsConcurrencyToken(true)
            .HasDefaultValue(null)
            .HasColumnName("last_modify_date");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("creation_date");

        builder
            .Property(x => x.DeletedAt)
            .HasDefaultValue(null)
            .HasColumnName("deleted_date");
    }
}