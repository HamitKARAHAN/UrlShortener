﻿// <copyright file="UrlShortenerDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Infrastructure.EntityFramework;

using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.TagDetails;
using UrlShortener.Domain.Tags;
using UrlShortener.InfrastructureCore.EntityFramework;

/// <inheritdoc />
public sealed class UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
    : SharedKernelDbContext<UrlShortenerDbContext>(options)
{
    public DbSet<Tag> Tags => this.Set<Tag>();
    public DbSet<TagDetail> TagDetails => this.Set<TagDetail>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Guard.Against.Null(modelBuilder);
        base.OnModelCreating(modelBuilder);

        modelBuilder.MapStronglyTypedUlid<Tag, TagId>(t => t.Id);
        modelBuilder.MapStronglyTypedUlid<TagDetail, TagDetailId>(t => t.Id);
        modelBuilder.Entity<Tag>().HasOne(t => t.TagDetail).WithOne().HasForeignKey<TagDetail>(td => td.TagId);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}