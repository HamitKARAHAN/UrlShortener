// <copyright file="Result.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore.Result;
using System.Net;
public class Result : ResultBase
{
    private Result(HttpStatusCode statusCode)
        : base(statusCode: statusCode) { }

    private Result(HttpStatusCode statusCode, Error error)
        : base(statusCode: statusCode, error: error) { }

    public static Result NotFound(string code, string message)
        => new (statusCode: HttpStatusCode.NotFound, error: Error.Create(code: code, message: message));

    public static Result BadRequest(string code, string message)
        => new (statusCode: HttpStatusCode.BadRequest, error: Error.Create(code: code, message: message));

    public static Result Redirect()
    => new (statusCode: HttpStatusCode.Redirect);

    public static Result Success()
        => new (statusCode: HttpStatusCode.NoContent);
}