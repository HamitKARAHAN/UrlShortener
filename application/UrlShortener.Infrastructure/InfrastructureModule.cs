// <copyright file="InfrastructureModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Infrastructure.EntityFramework;
using UrlShortener.InfrastructureCore;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureCoreModule<UrlShortenerDbContext>(configuration: configuration);
        return services;
    }
}
