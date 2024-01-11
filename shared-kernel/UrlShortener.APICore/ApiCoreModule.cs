// <copyright file="ApiCoreModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.APICore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using UrlShortener.APICore.Middlewares;

public static class ApiCoreModule
{
    private const int TimeoutStatusCode = (int)HttpStatusCode.GatewayTimeout;

    public static IServiceCollection AddApiCoreModule(this IServiceCollection services, Assembly assembly)
        => services
            .AddControllerModule()
            .AddSwaggerModule(assembly)
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

    [SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "<Pending>")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1404:Code analysis suppression should have justification", Justification = "<Pending>")]
    private static IServiceCollection AddSwaggerModule(this IServiceCollection services, Assembly assembly)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(name: "v1", info: new OpenApiInfo
            {
                Version = "v1",
                Title = "ToDo API",
                Description = "An ASP.NET Core Web API for managing ToDo items",
                TermsOfService = new Uri(uriString: "https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri(uriString: "https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });
            string xmlFilename = $"{assembly.GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        return services;
    }

    public static WebApplication UseApiCoreModule(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseForwardedHeaders();
        app.UseExceptionHandler(_ => { });
        app.UseRouting();
        app.MapControllers();
        return app;
    }
}
