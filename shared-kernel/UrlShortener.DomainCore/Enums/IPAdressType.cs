// <copyright file="IPAdressType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Enums;

using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IPAdressType
{
    /// <summary>
    /// None
    /// </summary>
    None = 0,

    /// <summary>
    /// AddressFamily.InterNetwork
    /// </summary>
    V4 = 1,

    /// <summary>
    /// AddressFamily.InterNetworkV6
    /// </summary>
    V6 = 2,
}
