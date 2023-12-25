// <copyright file="IShortCodeGenerator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Abstractions;

public interface IShortCodeGenerator
{
    Task<string> GenerateShortCode(string longUrl);
}
