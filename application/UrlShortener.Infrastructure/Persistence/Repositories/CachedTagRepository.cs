// <copyright file="CachedTagRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Domain.Abstractions;
using UrlShortener.Domain.Tags;
using UrlShortener.Infrastructure.Configurations;

namespace UrlShortener.Infrastructure.Persistence.Repositories;

internal sealed class CachedTagRepository(ITagRepository decorated, IMemoryCache cache, CacheSettings cacheSettings) : ITagRepository
{
    public async Task AddAsync(Tag aggregate, CancellationToken cancellationToken) => await decorated.AddAsync(aggregate, cancellationToken);

    public async Task<ShortCode> GetShortCodeAsync(string cacheKey, string longUrl, CancellationToken cancellationToken) => await cache
                .GetOrCreateAsync(cacheKey, entry =>
                {
                    entry.SetOptions(new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(cacheSettings.SlidingExpiration)
                            .SetAbsoluteExpiration(cacheSettings.AbsoluteExpiration)
                    .SetPriority(CacheItemPriority.Normal));

                    return decorated.GetShortCodeAsync(
                            cacheKey: cacheKey,
                            longUrl: longUrl,
                            cancellationToken: cancellationToken);
                });

    public async Task<LongUrl> GetLongUrlAsync(string cacheKey, string shortCode, CancellationToken cancellationToken) => await cache
                .GetOrCreateAsync(cacheKey, entry =>
                {
                    entry.SetOptions(new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(cacheSettings.SlidingExpiration)
                            .SetAbsoluteExpiration(cacheSettings.AbsoluteExpiration)
                    .SetPriority(CacheItemPriority.Normal));

                    return decorated.GetLongUrlAsync(
                            cacheKey: cacheKey,
                            shortCode: shortCode,
                            cancellationToken: cancellationToken);
                });
}
