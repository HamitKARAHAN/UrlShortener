// <copyright file="ITagRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq.Expressions;
using UrlShortener.Domain.Tags;

namespace UrlShortener.Domain.Abstractions;

public interface ITagRepository
{
    Task<Tag> GetAggregateByPredicateAsync(string key, Expression<Func<Tag, bool>> predicate, CancellationToken cancellationToken);

    Task AddAsync(Tag aggregate, CancellationToken cancellationToken);
}
