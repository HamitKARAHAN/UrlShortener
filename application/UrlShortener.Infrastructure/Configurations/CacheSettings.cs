// <copyright file="CacheSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Infrastructure.Configurations;

public sealed class CacheSettings
{
    public TimeSpan SlidingExpiration { get; set; }
    public TimeSpan AbsoluteExpiration { get; set; }
}