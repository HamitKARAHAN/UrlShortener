// <copyright file="Result{T}.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Result;

using System.Net;

public class Result<T> : ResultBase
{
    private Result(T value, HttpStatusCode statusCode)
        : base(statusCode: statusCode) => this.Value = value;

    private Result(HttpStatusCode statusCode, Error error)
        : base(statusCode: statusCode, error: error) { }

    public T Value { get; }

    public static Result<T> NotFound(Error error)
        => new (statusCode: HttpStatusCode.NotFound, error: error);

    public static Result<T> BadRequest(Error error)
        => new (statusCode: HttpStatusCode.BadRequest, error: error);

    public static Result<T> Success(T value)
        => new (value: value, statusCode: HttpStatusCode.OK);

    public static implicit operator Result<T>(T value)
        => Success(value: value);
}