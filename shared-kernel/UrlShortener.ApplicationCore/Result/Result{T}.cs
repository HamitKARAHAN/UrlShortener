using System.Net;

namespace UrlShortener.ApplicationCore.Result;
public class Result<T> : ResultBase
{
    private Result(T value, HttpStatusCode statusCode) : base(statusCode: statusCode) => this.Value = value;
    private Result(HttpStatusCode statusCode, Error error) : base(statusCode: statusCode, error: error) { }

    public T Value { get; }

    public static Result<T> NotFound(string code, string message) 
        => new(statusCode: HttpStatusCode.NotFound, error: Error.Create(code: code, message: message));
    public static Result<T> BadRequest(string code, string message) 
        => new(statusCode: HttpStatusCode.BadRequest, error: Error.Create(code: code, message: message));
    public static Result<T> Success(T value) 
        => new(value: value, statusCode: HttpStatusCode.OK);

    public static implicit operator Result<T>(T value) 
        => Success(value: value);
}