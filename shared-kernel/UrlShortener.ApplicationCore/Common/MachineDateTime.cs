// <copyright file="MachineDateTime.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore.Common;

using UrlShortener.DomainCore.Abstractions;

public class MachineDateTime : IDateTimeProvider
{
    DateTime IDateTimeProvider.UtcNow() => DateTime.UtcNow;
}
