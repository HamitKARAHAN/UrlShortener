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

    private Description(string description) => this.Value = description;

    private Description() { }

    public string Value { get; private set; }

    public static implicit operator string(Description description) => description?.Value ?? string.Empty;

    public static Result<Description> Create(string description)
    {
        Guard.Against.NullOrWhiteSpace(description);
        Guard.Against.NotLessThan(description.Length, MinLength, DomainErrors.TagErrors.Error1);
        Guard.Against.NotGreaterThan(description.Length, MaxLength, DomainErrors.TagErrors.Error2);
        return new Description(description);
    }
}
