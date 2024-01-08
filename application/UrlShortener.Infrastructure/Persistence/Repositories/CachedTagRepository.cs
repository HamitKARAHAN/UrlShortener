// <copyright file="CachedTagRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Domain.Abstractions;
using UrlShortener.Domain.Tags;

namespace UrlShortener.Infrastructure.Persistence.Repositories;

internal sealed class CachedTagRepository(ITagRepository decorated, IMemoryCache cache) : ITagRepository
{
    public Task AddAsync(Tag aggregate, CancellationToken cancellationToken) => decorated.AddAsync(aggregate, cancellationToken);

    public async Task<Tag> GetAggregateByPredicateAsync(string key, Expression<Func<Tag, bool>> predicate, CancellationToken cancellationToken)
        => await cache
            .GetOrCreateAsync($"tag-{key}", entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                return decorated.GetAggregateByPredicateAsync(key, predicate, cancellationToken);
            });
}
