﻿// <copyright file="ModelBindingExceptionMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.APICore.Middlewares;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

public class ModelBindingExceptionMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not BadHttpRequestException)
        {
            return false;
        }

        await Results
            .Problem(
                title: "Bad Request",
                detail: exception.Message,
                statusCode: (int)HttpStatusCode.BadRequest)
            .ExecuteAsync(httpContext);
        return true;
    }
}