﻿// <copyright file="SharedKernelDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.InfrastructureCore.EntityFramework;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
public abstract class SharedKernelDbContext<TContext>(DbContextOptions<TContext> options) : DbContext(options)
    where TContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        Guard.Against.Null(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensures that all enum properties are stored as strings in the database.
        modelBuilder.UseStringForEnums();
        base.OnModelCreating(modelBuilder);
    }
}
