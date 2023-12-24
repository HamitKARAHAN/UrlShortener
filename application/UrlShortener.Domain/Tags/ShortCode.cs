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

    private ShortCode(string value) => this.Value = value;

    public string Value { get; private set; }

    public static implicit operator string(ShortCode shortUrl) => shortUrl?.Value ?? string.Empty;

    public static Result<ShortCode> Create(string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        Guard.Against.EqualLength(value: value.Length, equalValue: ExactLength, DomainErrors.TagErrors.Error1);
        return Result<ShortCode>.Success(new ShortCode(value));
    }
}