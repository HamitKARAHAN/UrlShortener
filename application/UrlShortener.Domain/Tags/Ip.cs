// <copyright file="Ip.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using Ardalis.GuardClauses;
using System.Net;
using System.Net.Sockets;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Result;

public sealed record Ip : IValueObject
{
    private Ip(string value, IPAdressType type)
    {
        this.Value = value;
        this.Type = type;
    }

    public string Value { get; private set; }
    public IPAdressType Type { get; private set; }

    public static implicit operator string(Ip ipAdress) => ipAdress?.Value ?? string.Empty;

    public static Result<Ip> Create(string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        if (!IPAddress.TryParse(value, out IPAddress address))
        {
            return Result<Ip>.BadRequest(DomainErrors.TagErrors.Error1);
        }

        IPAdressType type = address.AddressFamily switch
        {
            AddressFamily.InterNetwork => IPAdressType.V4,
            AddressFamily.InterNetworkV6 => IPAdressType.V6,
            _ => IPAdressType.None
        };

        return Result<Ip>.Success(new(value, type));
    }
}

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