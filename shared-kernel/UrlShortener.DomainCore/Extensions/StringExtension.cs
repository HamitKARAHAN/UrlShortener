// <copyright file="StringExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Ardalis.GuardClauses;
using UrlShortener.DomainCore.Enums;

namespace UrlShortener.DomainCore.Extensions;

public static class StringExtension
{
    public static string ToPlural(this string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        return $"{value}s";
    }

    public static string GetHost(this string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        Uri uri = new (value);
        Guard.Against.Null(uri);
        return uri.Host;
    }

    public static Scheme GetScheme(this string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        Uri uri = new (value);
        Guard.Against.Null(uri);
        Enum.TryParse(typeof(Scheme), uri.Scheme, out object scheme);
        Guard.Against.Null(scheme);
        return (Scheme)scheme;
    }

    public static string NormalizeUrl(this string value, string schema)
    {
        Guard.Against.NullOrWhiteSpace(value);
        UriBuilder uriBuilder = new (schema, value);
        Guard.Against.Null(uriBuilder.Uri);
        return uriBuilder.Uri.AbsoluteUri;
    }
}