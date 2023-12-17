// <copyright file="GlobalExceptionMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.APICore.Middlewares;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

public class GlobalExceptionMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await Results
            .Problem(
                title: "Internal Server Error",
                detail: "Server Error",
                statusCode: (int)HttpStatusCode.InternalServerError)
            .ExecuteAsync(httpContext);

        return true;
    }
}