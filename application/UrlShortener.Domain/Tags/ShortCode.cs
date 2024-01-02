// <copyright file="ShortCode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using Ardalis.GuardClauses;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Extensions;
using UrlShortener.DomainCore.Result;

public sealed record ShortCode : IValueObject
{
    private const int ExactLength = 10;

    private ShortCode(string shortCode) => this.Value = shortCode;

    private ShortCode() { }

    public string Value { get; private set; }

    public static implicit operator string(ShortCode shortUrl) => shortUrl?.Value ?? string.Empty;

    public static Result<ShortCode> Create(string shortCode)
    {
        Guard.Against.NullOrWhiteSpace(shortCode);
        Guard.Against.EqualLength(value: shortCode.Length, equalValue: ExactLength, DomainErrors.TagErrors.Error1);
        return new ShortCode(shortCode);
    }
}