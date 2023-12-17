// <copyright file="ShortUrl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Ardalis.GuardClauses;
using UrlShortener.DomainCore.Abstractions;

namespace UrlShortener.Domain.Tags;

public sealed record ShortUrl : IValueObject
{
    public const int ExactLength = 25;
    private ShortUrl(string value) => this.Value = value;

    public string Value { get; private set; }

    public static implicit operator string(ShortUrl shortUrl) => shortUrl?.Value ?? string.Empty;

    public static ShortUrl Create(string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        Guard.Against.InvalidInput(value, nameof(Value), v => v.Length == ExactLength, "TODO");
        return new (value);
    }
}