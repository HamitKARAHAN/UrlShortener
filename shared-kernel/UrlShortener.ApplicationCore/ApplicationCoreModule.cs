// <copyright file="ApplicationCoreModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UrlShortener.ApplicationCore.Common;
using UrlShortener.DomainCore.Abstractions;

public static class ApplicationCoreModule
{
    public static IServiceCollection AddApplicationCoreModule(this IServiceCollection services, Assembly assembly)
    {
        services.AddTransient<IDateTimeProvider, MachineDateTime>();
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assembly));
        return services;
    }
}
