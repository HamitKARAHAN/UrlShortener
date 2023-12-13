// <copyright file="ResultBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore.Result;
using System.Net;
public class ResultBase
{
    protected ResultBase(HttpStatusCode statusCode)
    {
        this.IsSuccess = true;
        this.StatusCode = statusCode;
    }

    protected ResultBase(HttpStatusCode statusCode, Error error)
    {
        this.IsSuccess = false;
        this.StatusCode = statusCode;
        this.Error = error;
    }

    public HttpStatusCode StatusCode { get; }

    public bool IsSuccess { get; }

    public bool IsFailure => !this.IsSuccess;

    public Error Error { get; }
}