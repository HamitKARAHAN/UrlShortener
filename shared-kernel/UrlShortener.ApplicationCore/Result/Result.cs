using System.Net;

namespace UrlShortener.ApplicationCore.Result;
public class Result : ResultBase
{
    private Result(HttpStatusCode statusCode) : base(statusCode: statusCode) { }

    private Result(HttpStatusCode statusCode, Error error) : base(statusCode: statusCode, error: error) { }

    public static Result NotFound(string code, string message) 
        => new (statusCode: HttpStatusCode.NotFound, error: Error.Create(code: code, message: message));
    public static Result BadRequest(string code, string message)
        => new(statusCode: HttpStatusCode.BadRequest, error: Error.Create(code: code, message: message));
    public static Result Success()
        => new(statusCode: HttpStatusCode.NoContent);
}