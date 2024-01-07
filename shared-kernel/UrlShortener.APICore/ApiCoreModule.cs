// <copyright file="ApiCoreModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.APICore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using UrlShortener.APICore.Middlewares;

public static class ApiCoreModule
{
    private const int TimeoutStatusCode = (int)HttpStatusCode.GatewayTimeout;

    public static IServiceCollection AddApiCoreModule(this IServiceCollection services)
        => services
            .AddControllerModule()
            .AddForwardedHeaders()
            .AddRequestTimeouts()
            .AddExceptionHandlerMiddlewares();

    private static IServiceCollection AddControllerModule(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.Formatting = Formatting.Indented;
            });
        return services;
    }

    private static IServiceCollection AddForwardedHeaders(this IServiceCollection services)
        => services
            .AddHttpContextAccessor()
            .Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);

    private static IServiceCollection AddRequestTimeouts(this IServiceCollection services)
        => services.AddRequestTimeouts(options
            => options.DefaultPolicy = new RequestTimeoutPolicy
            {
                Timeout = TimeSpan.FromMilliseconds(value: 500),
                TimeoutStatusCode = TimeoutStatusCode,
                WriteTimeoutResponse = async (HttpContext context)
                    => await Results
                        .Problem(
                            title: "Request Timeout",
                            detail: $"{context.Request.Method} {context.Request.Path} {context.Request.QueryString}".Trim(),
                            statusCode: TimeoutStatusCode)
                        .ExecuteAsync(context)
            });

    private static IServiceCollection AddExceptionHandlerMiddlewares(this IServiceCollection services)
        => services
            .AddExceptionHandler<ModelBindingExceptionMiddleware>()
            .AddExceptionHandler<GlobalExceptionMiddleware>();

    public static WebApplication UseApiCoreModule(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseForwardedHeaders();
        app.UseRequestTimeouts();
        app.UseExceptionHandler(_ => { });
        app.UseRouting();
        app.MapControllers();
        return app;
    }
}
