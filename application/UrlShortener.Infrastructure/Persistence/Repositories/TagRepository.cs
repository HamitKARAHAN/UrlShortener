// <copyright file="TagRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Abstractions;
using UrlShortener.Domain.Tags;
using UrlShortener.DomainCore.Extensions;
using UrlShortener.Infrastructure.Persistence.EntityFramework;
using UrlShortener.InfrastructureCore.Persistence;

namespace UrlShortener.Infrastructure.Persistence.Repositories;

internal sealed class TagRepository(UrlShortenerDbContext dbContext)
    : BaseRepository<Tag, TagId>(dbContext), ITagRepository
{
    public async Task<ShortCode> GetShortCodeAsync(string cacheKey, string longUrl, CancellationToken cancellationToken)
        => await dbContext
            .Set<Tag>()
            .Where(x => x.LongUrl == LongUrl.Create(longUrl.GetScheme(), longUrl.GetHost()).Value)
            .Select(x => x.ShortCode)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<LongUrl> GetLongUrlAsync(string cacheKey, string shortCode, CancellationToken cancellationToken)
    => await dbContext
            .Set<Tag>()
            .Where(x => x.ShortCode == ShortCode.Create(shortCode).Value)
            .Select(x => x.LongUrl)
            .FirstOrDefaultAsync(cancellationToken);
}
