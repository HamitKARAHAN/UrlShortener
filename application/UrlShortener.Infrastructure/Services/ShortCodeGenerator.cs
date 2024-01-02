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

internal sealed class ShortCodeGenerator(IDateTimeProvider dateTimeProvider, IOptions<UrlShortenerSettings> options) : IShortCodeGenerator
{
    private readonly UrlShortenerSettings settings = options.Value;
    public async Task<string> GenerateShortCode(string longUrl)
    {
        string combinedString = $"{longUrl}{dateTimeProvider.UtcNow.Ticks}";

        string hashedString = await GetHashString(combinedString);
        if (hashedString.Length < this.settings.ShortCodeLenght)
        {
            hashedString = hashedString.PadRight(this.settings.ShortCodeLenght, '0');
        }
        else if (hashedString.Length > this.settings.ShortCodeLenght)
        {
            hashedString = hashedString[..this.settings.ShortCodeLenght];
        }

        return hashedString;
    }

    public string GenerateUrl(string shortCode)
        => $"{this.settings.BaseUrl}/{shortCode}";

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
