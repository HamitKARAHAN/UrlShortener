// <copyright file="BaseRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using UrlShortener.DomainCore.Primitives;

namespace UrlShortener.InfrastructureCore.Persistence;

public abstract class BaseRepository<T, TId>(DbContext dbContext)
    where T : AggregateRoot<TId>
    where TId : IComparable<TId>
{
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable SA1401 // Fields should be private
    protected readonly DbSet<T> DbSet = dbContext.Set<T>();
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA1051 // Do not declare visible instance fields

    public async Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        object[] keyValues = [id];
        return await this.DbSet.FindAsync(keyValues, cancellationToken);
    }

    public async Task<T> GetAggregateByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        => await this.DbSet.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<TProperty> GetPropertyBySelector<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> selector, CancellationToken cancellationToken) => await this.DbSet.Where(predicate).Select(selector).FirstOrDefaultAsync(cancellationToken);

    public async Task<IList<T>> GetAggregatesByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    => await this.DbSet.Where(predicate).ToListAsync(cancellationToken);

    public async Task<bool> ExistsAsync(TId id, CancellationToken cancellationToken)
        => this.DbSet.Local.Any(e => e.Id.Equals(id)) || await this.DbSet.AnyAsync(e => e.Id.Equals(id), cancellationToken);

    public async Task AddAsync(T aggregate)
    {
        Guard.Against.Null(aggregate);
        await this.DbSet.AddAsync(aggregate);
    }

    public void Update(T aggregate)
    {
        Guard.Against.Null(aggregate);
        this.DbSet.Update(aggregate);
    }

    public async Task BulkUpdate(Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> propertiesExpression, CancellationToken cancellationToken)
        => await this.DbSet.ExecuteUpdateAsync(propertiesExpression, cancellationToken);

    public void Remove(T aggregate)
    {
        Guard.Against.Null(aggregate);
        this.DbSet.Remove(aggregate);
    }

    public async Task BulkDelete(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        => await this.DbSet.Where(predicate).ExecuteDeleteAsync(cancellationToken);
}
