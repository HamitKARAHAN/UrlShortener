// <copyright file="CacheSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Infrastructure.Configurations;

public sealed class CacheSettings
{
    [Required]
    [Range(typeof(TimeSpan), minimum: "1.00:00:00", maximum: "60.00:00:00")]
    public TimeSpan SlidingExpiration { get; set; }

    [Required]
    [Range(typeof(TimeSpan), minimum: "00:30:00", maximum: "06:00:00")]
    public TimeSpan AbsoluteExpiration { get; set; }
}