// <copyright file="GetTag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UrlShortener.ApplicationCore.CQRS;
using UrlShortener.DomainCore.Result;

namespace UrlShortener.Application.Features.Tags.GetTag;
public static class GetTag
{
    public sealed record Query(string ShortCode) : IQuery<Result<string>>;

    public sealed class Handler : IQueryHandler<Query, Result<string>>
    {
        public Task<Result<string>> Handle(Query query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
