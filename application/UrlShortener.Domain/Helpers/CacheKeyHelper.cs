// <copyright file="CacheKeyHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using UrlShortener.DomainCore.Extensions;

namespace UrlShortener.Domain.Helpers;

[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1404:Code analysis suppression should have justification", Justification = "<Pending>")]
[SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
[SuppressMessage("Critical Code Smell", "S2223:Non-constant static fields should not be visible", Justification = "<Pending>")]
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Pending>")]
[SuppressMessage("Minor Code Smell", "S1104:Fields should not have public accessibility", Justification = "<Pending>")]
public static class CacheKeyHelper
{
    public static Func<string, string> GetShortCodeByLongUrl = longUrl => $"tag_scheme_{longUrl.GetScheme()}_host_{longUrl.GetHost()}";
    public static Func<string, string> GetLongUrlByShortCode = shortCode => $"tag_short_code_{shortCode}";
}
