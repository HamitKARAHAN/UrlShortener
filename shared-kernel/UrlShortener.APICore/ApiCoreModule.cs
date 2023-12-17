// <copyright file="ApiCoreModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.APICore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using UrlShortener.APICore.Middlewares;

public static class ApiCoreModule
{
    private const int StatusCode = (int)HttpStatusCode.GatewayTimeout;
    public static IServiceCollection AddApiCoreModule(this IServiceCollection services)
        => services
            .AddRequestTimeouts()
            .AddExceptionHandlerMiddlewares();

    private static IServiceCollection AddRequestTimeouts(this IServiceCollection services)
        => services.AddRequestTimeouts(options
            => options.DefaultPolicy = new RequestTimeoutPolicy
            {
                Timeout = TimeSpan.FromMilliseconds(value: 500),
                TimeoutStatusCode = StatusCode,
                WriteTimeoutResponse = async (HttpContext context)
                    => await Results
                        .Problem(
                            title: "Request Timeout",
                            detail: $"{context.Request.Method} {context.Request.Path} {context.Request.QueryString}".Trim(),
                            statusCode: StatusCode)
                        .ExecuteAsync(context)
            });

    private static IServiceCollection AddExceptionHandlerMiddlewares(this IServiceCollection services)
        => services
            .AddExceptionHandler<ModelBindingExceptionMiddleware>()
            .AddExceptionHandler<GlobalExceptionMiddleware>();

    public static WebApplication UseApiCoreModule(this WebApplication app)
    {
        app.UseRequestTimeouts();
        app.UseExceptionHandler(_ => { });
        return app;
    }
}
