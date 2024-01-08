// <copyright file="InfrastructureModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Infrastructure;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Domain.Abstractions;
using UrlShortener.Infrastructure.Configurations;
using UrlShortener.Infrastructure.Persistence.EntityFramework;
using UrlShortener.Infrastructure.Persistence.Repositories;
using UrlShortener.Infrastructure.Services;
using UrlShortener.InfrastructureCore;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddMemoryCacheModule()
            .AddConfigurationModule()
            .AddDI()
            .AddInfrastructureCoreModule(configuration: configuration);

    public static IServiceCollection AddDI(this IServiceCollection services)
        => services
            .AddSingleton<IShortCodeGenerator, ShortCodeGenerator>()
            .AddScoped<ITagRepository>(provider =>
            {
                UrlShortenerDbContext context = provider.GetService<UrlShortenerDbContext>();
                return new CachedTagRepository(
                    decorated: new TagRepository(context),
                    cache: provider.GetService<IMemoryCache>(),
                    cacheSettings: provider.GetService<CacheSettings>());
            });

    private static IServiceCollection AddInfrastructureCoreModule(this IServiceCollection services, IConfiguration configuration)
        => services.AddInfrastructureCoreModule<UrlShortenerDbContext>(configuration: configuration);

    private static IServiceCollection AddMemoryCacheModule(this IServiceCollection services) => services.AddMemoryCache();

    private static IServiceCollection AddConfigurationModule(this IServiceCollection services)
    {
        services
            .AddOptions<UrlShortenerSettings>()
            .BindConfiguration(nameof(UrlShortenerSettings))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        return services;
    }
}
