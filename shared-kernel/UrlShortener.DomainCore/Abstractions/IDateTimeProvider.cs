// <copyright file="IDateTimeProvider.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Abstractions;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
