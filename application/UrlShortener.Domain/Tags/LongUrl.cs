// <copyright file="LongUrl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using Ardalis.GuardClauses;
using UrlShortener.ApplicationCore.Result;
using UrlShortener.Domain.Extensions;
using UrlShortener.DomainCore.Abstractions;

public sealed record LongUrl : IValueObject
{
    private LongUrl(string value) => this.Value = value;

    public string Value { get; private set; }

    public static implicit operator string(LongUrl shortUrl) => shortUrl?.Value ?? string.Empty;

    public static Result<LongUrl> Create(string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        Guard.Against.InValidUrlCanBeUpperCase(value: value, message: "TODO");
        return Result<LongUrl>.Success(value: new LongUrl(value));
    }
}