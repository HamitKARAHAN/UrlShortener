// <copyright file="CachedTagRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Domain.Abstractions;
using UrlShortener.Domain.Tags;
using UrlShortener.Infrastructure.Configurations;

namespace UrlShortener.Infrastructure.Persistence.Repositories;

internal sealed class CachedTagRepository(ITagRepository decorated, IMemoryCache cache, CacheSettings cacheSettings) : ITagRepository
{
    public async Task AddAsync(Tag aggregate) => await decorated.AddAsync(aggregate);

    public async Task<ShortCode> GetShortCodeAsync(string cacheKey, string longUrl, CancellationToken cancellationToken) => await cache
                .GetOrCreateAsync(cacheKey, async entry =>
                {
                    ShortCode shortCode = await decorated.GetShortCodeAsync(
                        cacheKey: cacheKey,
                        longUrl: longUrl,
                        cancellationToken: cancellationToken);

                    if (shortCode == null)
                    {
                        entry.Dispose();
                        return null;
                    }

                    entry.Value = shortCode;
                    entry.SetOptions(new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(cacheSettings.SlidingExpiration)
                            .SetAbsoluteExpiration(cacheSettings.AbsoluteExpiration)
                            .SetPriority(CacheItemPriority.Normal));
                    return shortCode;
                });

    public async Task<LongUrl> GetLongUrlAsync(string cacheKey, string shortCode, CancellationToken cancellationToken) => await cache
                .GetOrCreateAsync(cacheKey, async entry =>
                {
                    LongUrl longUrl = await decorated.GetLongUrlAsync(
                            cacheKey: cacheKey,
                            shortCode: shortCode,
                            cancellationToken: cancellationToken);

                    if (longUrl == null)
                    {
                        entry.Dispose();
                        return null;
                    }

                    entry.Value = longUrl;
                    entry.SetOptions(new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(cacheSettings.SlidingExpiration)
                            .SetAbsoluteExpiration(cacheSettings.AbsoluteExpiration)
                            .SetPriority(CacheItemPriority.Normal));
                    return longUrl;
                });
}
