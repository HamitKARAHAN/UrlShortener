// <copyright file="LongUrl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using Ardalis.GuardClauses;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Extensions;
using UrlShortener.DomainCore.Result;

public sealed record LongUrl : IValueObject
{
    private LongUrl(string longUrl) => this.Value = longUrl;

    private LongUrl() { }

    public string Value { get; private set; }

    public static implicit operator string(LongUrl shortUrl) => shortUrl?.Value ?? string.Empty;

    public static Result<LongUrl> Create(string longUrl)
    {
        Guard.Against.NullOrWhiteSpace(longUrl);
        Guard.Against.InValidUrlCanBeUpperCase(value: longUrl, DomainErrors.TagErrors.Error2);
        return new LongUrl(longUrl);
    }
}