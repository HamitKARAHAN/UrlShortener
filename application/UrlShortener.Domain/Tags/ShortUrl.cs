// <copyright file="ShortUrl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using Ardalis.GuardClauses;
using UrlShortener.ApplicationCore.Result;
using UrlShortener.Domain.Extensions;
using UrlShortener.DomainCore.Abstractions;

public sealed record ShortUrl : IValueObject
{
    private const int ExactLength = 25;

    private ShortUrl(string value) => this.Value = value;

    public string Value { get; private set; }

    public static implicit operator string(ShortUrl shortUrl) => shortUrl?.Value ?? string.Empty;

    public static Result<ShortUrl> Create(string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        Guard.Against.EqualLength(value: value.Length, equalValue: ExactLength, message: "TODO");
        Guard.Against.InValidUrl(value: value, message: "TODO");
        return Result<ShortUrl>.Success(new ShortUrl(value));
    }
}