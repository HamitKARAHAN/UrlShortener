using Ardalis.GuardClauses;

namespace UrlShortener.Domain.Extensions;

public static class StringExtension
{
    public static string ToPlural(this string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        return $"{value}s";
    }
}
