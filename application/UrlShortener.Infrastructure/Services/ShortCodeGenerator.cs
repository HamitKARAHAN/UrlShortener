// <copyright file="ShortCodeGenerator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using UrlShortener.Domain.Abstractions;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.Infrastructure.Configurations;

namespace UrlShortener.Infrastructure.Services;

public sealed class ShortCodeGenerator(IDateTimeProvider dateTimeProvider, IOptions<UrlShortenerSettings> options) : IShortCodeGenerator
{
    public async Task<string> GenerateShortCode(string longUrl)
    {
        UrlShortenerSettings settings = options.Value;
        string combinedString = $"{longUrl}{dateTimeProvider.UtcNow.Ticks}";

        string hashedString = await GetHashString(combinedString);
        if (hashedString.Length < settings.ShortCodeLenght)
        {
            hashedString = hashedString.PadRight(settings.ShortCodeLenght, '0');
        }
        else if (hashedString.Length > settings.ShortCodeLenght)
        {
            hashedString = hashedString[..settings.ShortCodeLenght];
        }

        return hashedString;
    }

    private static async Task<string> GetHashString(string input)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] data = Encoding.UTF8.GetBytes(input);
        using MemoryStream stream = new (data);
        Memory<byte> result = new byte[32];
        await SHA256.HashDataAsync(stream, result);
        byte[] hashedBytes = result.ToArray();
        return BitConverter
            .ToString(hashedBytes)
            .Replace(oldValue: "-", newValue: string.Empty, comparisonType: StringComparison.OrdinalIgnoreCase)
            .ToLowerInvariant();
    }
}
