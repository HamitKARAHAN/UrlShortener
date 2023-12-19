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

    public static Result NotFound(Error error)
        => new (statusCode: HttpStatusCode.NotFound, error: error);

    public static Result BadRequest(Error error)
        => new (statusCode: HttpStatusCode.BadRequest, error: error);

    public static Result Redirect()
    => new (statusCode: HttpStatusCode.Redirect);

    public static Result Success()
        => new (statusCode: HttpStatusCode.NoContent);
}