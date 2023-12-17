// <copyright file="ApplicationModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Application;

using Microsoft.Extensions.DependencyInjection;
using UrlShortener.ApplicationCore;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddApplicationCoreModule(AssemblyReference.Assembly);
        return services;
    }
}
