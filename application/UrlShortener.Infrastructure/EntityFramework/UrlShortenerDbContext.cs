// <copyright file="UrlShortenerDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using UrlShortener.InfrastructureCore.EntityFramework;

/// <inheritdoc />
public sealed class UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
    : SharedKernelDbContext<UrlShortenerDbContext>(options)
{
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}