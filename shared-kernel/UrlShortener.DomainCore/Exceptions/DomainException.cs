using UrlShortener.DomainCore.Result;

namespace UrlShortener.DomainCore.Exceptions;

public sealed class DomainException(Error error, string paramName) : Exception
{
    public Error Error { get; private set; } = error;
    public string ParameterName { get; set; } = paramName;
}
