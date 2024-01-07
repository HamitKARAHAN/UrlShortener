// <copyright file="ApplicationCoreModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UrlShortener.ApplicationCore.Behaviors;
using UrlShortener.ApplicationCore.Common;
using UrlShortener.DomainCore.Abstractions;

public static class ApplicationCoreModule
{
    public static IServiceCollection AddApplicationCoreModule(this IServiceCollection services, Assembly assembly)
    {
        services.AddTransient<IDateTimeProvider, MachineDateTime>();
        services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(config =>
        {
            config.AddOpenBehavior(typeof(ClientInfoBehavior<>));
            config.AddOpenBehavior(typeof(ValidationBehavior<>));
        });
        return services;
    }
}
