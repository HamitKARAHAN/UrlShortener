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

    internal HttpStatusCode StatusCode { get; }

    internal bool IsSuccess { get; }

    internal bool IsFailure => !this.IsSuccess;

    internal Error Error { get; }
}