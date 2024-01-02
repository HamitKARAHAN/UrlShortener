﻿// <copyright file="GetTag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using UrlShortener.ApplicationCore.CQRS;
using UrlShortener.Domain.Abstractions;
using UrlShortener.Domain.Tags;
using UrlShortener.DomainCore.Result;

namespace UrlShortener.Application.Features.Tags.GetTag;
public static class GetTag
{
    public sealed record Query(string ShortCode) : IQuery<Result<string>>;

    internal sealed class Handler(ITagRepository tagRepository) : IQueryHandler<Query, Result<string>>
    {
        public async ValueTask<Result<string>> Handle(Query query, CancellationToken cancellationToken)
        {
            Tag tag = await tagRepository.GetAggregateByPredicateAsync(x => x.ShortCode == query.ShortCode, cancellationToken);

            if (tag is null)
            {
                return Result<string>.BadRequest(DomainErrors.TagErrors.Error1);
            }

            return Result<string>.Success(tag.LongUrl);
        }
    }
}

public sealed class GetTagValidator : AbstractValidator<GetTag.Query>
{
    public GetTagValidator()
    {
        this.RuleFor(x => x.ShortCode).NotEmpty();
        this.RuleFor(x => x.ShortCode).Length(10);
    }
}
