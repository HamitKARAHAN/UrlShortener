// <copyright file="Ip.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using Ardalis.GuardClauses;
using System.Net;
using System.Net.Sockets;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Enums;
using UrlShortener.DomainCore.Extensions;
using UrlShortener.DomainCore.Result;

public sealed record Ip : IValueObject
{
    private Ip(string ipAddress, IPAdressType type)
    {
        this.Value = ipAddress;
        this.Type = type;
    }

    private Ip() { }

    public string Value { get; private set; }
    public IPAdressType Type { get; private set; }

    public static implicit operator string(Ip ipAdress) => ipAdress?.Value ?? string.Empty;

    public static Result<Ip> Create(string ipAddress)
    {
        Guard.Against.NullOrWhiteSpace(ipAddress);
        Guard.Against.InvalidIp(ipAddress, DomainErrors.TagErrors.Error1, out IPAddress address);

        IPAdressType type = address.AddressFamily switch
        {
            AddressFamily.InterNetwork => IPAdressType.V4,
            AddressFamily.InterNetworkV6 => IPAdressType.V6,
            _ => IPAdressType.None
        };
        return new Ip(ipAddress, type);
    }
}