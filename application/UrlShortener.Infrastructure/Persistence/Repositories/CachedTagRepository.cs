// <copyright file="CachedTagRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Domain.Abstractions;
using UrlShortener.Domain.Tags;
using UrlShortener.Infrastructure.Configurations;

namespace UrlShortener.Infrastructure.Persistence.Repositories;

internal sealed class CachedTagRepository(ITagRepository decorated, IMemoryCache cache, CacheSettings cacheSettings) : ITagRepository
{
    public async Task AddAsync(Tag aggregate, CancellationToken cancellationToken) => await decorated.AddAsync(aggregate, cancellationToken);

    public async Task<Tag> GetAggregateByPredicateAsync(string key, Expression<Func<Tag, bool>> predicate, CancellationToken cancellationToken) => await cache
                .GetOrCreateAsync(key, entry =>
                {
                    entry.SetOptions(new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(cacheSettings.SlidingExpiration)
                            .SetAbsoluteExpiration(cacheSettings.AbsoluteExpiration)
                            .SetPriority(CacheItemPriority.Normal));

                    return decorated.GetAggregateByPredicateAsync(key, predicate, cancellationToken);
                });
}
