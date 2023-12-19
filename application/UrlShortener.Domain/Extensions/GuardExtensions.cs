// <copyright file="GuardExtensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Extensions;

using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using UrlShortener.Domain.Tags;

public static partial class GuardExtensions
{
    [GeneratedRegex(@"^(https?|ftp):\/\/[^\s\/$.?#].[^\s]*$", RegexOptions.IgnoreCase, "tr-TR")]
    private static partial Regex ValidUrl();
    public static void InValidUrl(this IGuardClause guardClause, string value, string message, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (!ValidUrl().IsMatch(value) || value.Any(char.IsUpper))
        {
            throw new ArgumentException(message, parameterName);
        }
    }

    public static void InValidUrlCanBeUpperCase(this IGuardClause guardClause, string value, string message, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (!ValidUrl().IsMatch(value))
        {
            throw new ArgumentException(message, parameterName);
        }
    }

    public static void NotGreaterThan(this IGuardClause guardClause, int value, int maxValue, string message, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (value > maxValue)
        {
            throw new ArgumentException(message, parameterName);
        }
    }

    public static void NotLessThan(this IGuardClause guardClause, int value, int minValue, string message, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (value < minValue)
        {
            throw new ArgumentException(message, parameterName);
        }
    }

    public static void EqualLength(this IGuardClause guardClause, int value, int equalValue, string message, [CallerArgumentExpression(nameof(value))] string parameterName = null)
    {
        if (value != equalValue)
        {
            throw new ArgumentException(message, parameterName);
        }
    }
}
