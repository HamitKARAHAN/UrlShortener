// <copyright file="Scheme.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Enums;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Scheme
{
    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// Stands for HTTP schema
    /// </summary>
#pragma warning disable SA1300 // Element should begin with upper-case letter
    http = 1,
#pragma warning restore SA1300 // Element should begin with upper-case letter

    /// <summary>
    /// Stands for HTTPS schema
    /// </summary>
#pragma warning disable SA1300 // Element should begin with upper-case letter
    https = 2
#pragma warning restore SA1300 // Element should begin with upper-case letter
}
