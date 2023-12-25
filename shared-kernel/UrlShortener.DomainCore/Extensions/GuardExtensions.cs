// <copyright file="GuardExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Extensions;

using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using UrlShortener.DomainCore.Exceptions;
using UrlShortener.DomainCore.Result;

public static partial class GuardExtensions
{
    [GeneratedRegex(@"^(https?|ftp):\/\/[^\s\/$.?#].[^\s]*$", RegexOptions.IgnoreCase, "tr-TR")]
    private static partial Regex ValidUrl();

    public static void InValidUrl(this IGuardClause guardClause, string value, Error error, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (!ValidUrl().IsMatch(value) || value.Any(char.IsUpper))
        {
            throw new DomainException(error, parameterName);
        }
    }

    public static void InValidUrlCanBeUpperCase(this IGuardClause guardClause, string value, Error error, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (!ValidUrl().IsMatch(value))
        {
            throw new DomainException(error, parameterName);
        }
    }

    public static void NotGreaterThan(this IGuardClause guardClause, int value, int maxValue, Error error, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (value > maxValue)
        {
            throw new DomainException(error, parameterName);
        }
    }

    public static void NotLessThan(this IGuardClause guardClause, int value, int minValue, Error error, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (value < minValue)
        {
            throw new DomainException(error, parameterName);
        }
    }

    public static void EqualLength(this IGuardClause guardClause, int value, int equalValue, Error error, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (value != equalValue)
        {
            throw new DomainException(error, parameterName);
        }
    }
}
