// <copyright file="ITagRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;
using UrlShortener.Domain.Tags;

namespace UrlShortener.Domain.Abstractions;

public interface ITagRepository
{
    Task<ShortCode> GetShortCodeAsync(string cacheKey, string longUrl, CancellationToken cancellationToken);

    Task<LongUrl> GetLongUrlAsync(string cacheKey, string shortCode, CancellationToken cancellationToken);

    Task AddAsync(Tag aggregate);
}
