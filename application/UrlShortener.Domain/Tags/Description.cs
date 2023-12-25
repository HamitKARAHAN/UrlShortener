// <copyright file="Description.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Ardalis.GuardClauses;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Extensions;
using UrlShortener.DomainCore.Result;

namespace UrlShortener.Domain.Tags;

public sealed record Description : IValueObject
{
    private const int MinLength = 10;
    private const int MaxLength = 50;

    private Description(string value) => this.Value = value;

    public string Value { get; private set; }

    public static implicit operator string(Description description) => description?.Value ?? string.Empty;

    public static Result<Description> Create(string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        Guard.Against.NotLessThan(value.Length, MinLength, DomainErrors.TagErrors.Error1);
        Guard.Against.NotGreaterThan(value.Length, MaxLength, DomainErrors.TagErrors.Error2);
        return Result<Description>.Success(new Description(value));
    }
}
