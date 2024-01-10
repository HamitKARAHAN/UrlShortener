// <copyright file="LongUrl.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using Ardalis.GuardClauses;
using System.Runtime.CompilerServices;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Enums;
using UrlShortener.DomainCore.Extensions;
using UrlShortener.DomainCore.Result;

public sealed record LongUrl : IValueObject
{
    private LongUrl(Scheme scheme, string host)
    {
        this.Scheme = scheme;
        this.Host = host;
    }

    private LongUrl() { }

    public string Host { get; private set; }
    public Scheme Scheme { get; private set; }

    public static implicit operator string(LongUrl longUrl)
        => (longUrl is not null && longUrl.Scheme != Scheme.None && !string.IsNullOrWhiteSpace(longUrl.Host)) ? ConcatUrl(longUrl.Scheme, longUrl.Host) : string.Empty;

    public static Result<LongUrl> Create(Scheme scheme, string host)
    {
        Guard.Against.Default(scheme);
        Guard.Against.NullOrWhiteSpace(host);
        Guard.Against.InValidUrlCanBeUpperCase(value: ConcatUrl(scheme, host), DomainErrors.TagErrors.Error2);

        return new LongUrl(scheme: scheme, host: host);
    }

    public string ConcatLongUrl()
        => ConcatUrl(this.Scheme, this.Host);

    private static string ConcatUrl(Scheme scheme, string host)
        => string.Concat(scheme.ToString(), "://", host);
}