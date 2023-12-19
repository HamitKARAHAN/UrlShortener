using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;
using UrlShortener.ApplicationCore.Result;
using UrlShortener.Domain.Extensions;
using UrlShortener.DomainCore.Abstractions;

namespace UrlShortener.Domain.Tags;

public sealed record Description : IValueObject
{
    private const int MinLength = 10;
    private const int MaxLength = 50;

    private Description(string value) => this.Value = value;

    public string Value { get; private set; }

    public static implicit operator string(Description description) => description?.Value ?? string.Empty;

    public static Result<Description> Create(string value)
    {
        Guard.Against.NullOrWhiteSpace(value);
        Guard.Against.NotLessThan(value.Length, MinLength, "TODO");
        Guard.Against.NotGreaterThan(value.Length, MaxLength, "TODO");
        return Result<Description>.Success(new Description(value));
    }
}
