// <copyright file="TagRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;
using UrlShortener.Domain.Abstractions;
using UrlShortener.Domain.Tags;
using UrlShortener.Infrastructure.Persistence.EntityFramework;
using UrlShortener.InfrastructureCore.Persistence;

namespace UrlShortener.Infrastructure.Persistence.Repositories;

internal sealed class TagRepository(UrlShortenerDbContext dbContext)
    : BaseRepository<Tag, TagId>(dbContext), ITagRepository
{
    public Task<Tag> GetAggregateByPredicateAsync(string key, Expression<Func<Tag, bool>> predicate, CancellationToken cancellationToken) => this.GetAggregateByPredicateAsync(predicate, cancellationToken);
}
