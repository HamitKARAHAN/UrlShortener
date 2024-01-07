// <copyright file="UrlShortenerSettings.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Infrastructure.Configurations;

public sealed class UrlShortenerSettings
{
    [Required]
    [AllowedValues(10)]
    public int ShortCodeLenght { get; set; }

    [Required]
    [Url]
    [Length(minimumLength: 20, maximumLength: 20)]
    public string BaseUrl { get; set; }
}
