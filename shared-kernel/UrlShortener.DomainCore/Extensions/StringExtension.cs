// <copyright file="StringExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Ardalis.GuardClauses;

namespace UrlShortener.DomainCore.Extensions;

public static class StringExtension
{
    public static string ToPlural(this string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        return $"{value}s";
    }
}