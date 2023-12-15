// <copyright file="SoftDeleteInterceptor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.InfrastructureCore.EntityFramework;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Primitives;
internal sealed class SoftDeleteInterceptor(IDateTimeProvider dateTimeProvider) : SaveChangesInterceptor
{
    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        this.SetSoftDelete(eventData);
        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        this.SetSoftDelete(eventData);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private void SetSoftDelete(SaveChangesCompletedEventData eventData)
    {
        Guard.Against.Null(eventData.Context);
        IEnumerable<EntityEntry<ISoftDelete>> entries = eventData.Context.ChangeTracker.Entries<ISoftDelete>();

        foreach (EntityEntry<ISoftDelete> entry in entries)
        {
            if (entry is { State: EntityState.Deleted, Entity: ISoftDelete softDeleteEntity })
            {
                entry.State = EntityState.Modified;
                softDeleteEntity.SetDeleted(dateTimeProvider.UtcNow);
            }
        }
    }
}
