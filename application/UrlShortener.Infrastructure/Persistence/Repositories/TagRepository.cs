// <copyright file="TagRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UrlShortener.Domain.Abstractions;
using UrlShortener.Domain.Tags;
using UrlShortener.Infrastructure.Persistence.EntityFramework;
using UrlShortener.InfrastructureCore.Persistence;

namespace UrlShortener.Infrastructure.Persistence.Repositories;

internal sealed class TagRepository(UrlShortenerDbContext dbContext)
    : BaseRepository<Tag, TagId>(dbContext), ITagRepository
{
}
