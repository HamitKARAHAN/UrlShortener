// <copyright file="UpdateAuditableEntitiesInterceptor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.InfrastructureCore.EntityFramework;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.DomainCore.Primitives;
internal sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAutditableEntites(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAutditableEntites(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAutditableEntites(DbContextEventData eventData)
    {
        Guard.Against.Null(eventData.Context);
        IEnumerable<EntityEntry<IAuditableEntity>> entries = eventData.Context.ChangeTracker.Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {
            switch (entityEntry.State)
            {
                case EntityState.Modified:
                    entityEntry.Entity.UpdateModifiedAt(DateTime.UtcNow);
                    break;
                case EntityState.Added:
                    Guard.Against.Default(entityEntry.Entity.CreatedAt);
                    break;
                default:
                    break;
            }
        }
    }
}