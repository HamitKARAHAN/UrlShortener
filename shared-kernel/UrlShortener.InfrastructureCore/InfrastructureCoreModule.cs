// <copyright file="InfrastructureCoreModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.InfrastructureCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.InfrastructureCore.EntityFramework;
using UrlShortener.InfrastructureCore.Persistence;

public static class InfrastructureCoreModule
{
    public static IServiceCollection AddInfrastructureCoreModule<T>(this IServiceCollection services, IConfiguration configuration)
        where T : DbContext
        => services
            .AddDI()
            .ConfigureDatabaseContext<T>(configuration: configuration);

    private static IServiceCollection ConfigureDatabaseContext<T>(
        this IServiceCollection services,
        IConfiguration configuration)
        where T : DbContext
    {
        services
            .AddDbContext<T>((IServiceProvider sp, DbContextOptionsBuilder options)
             => options
                 .UseSqlServer(configuration.GetConnectionString(name: "Sql"))
                 .AddInterceptors(
                     sp.GetRequiredService<SoftDeleteInterceptor>(),
                     sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>(),
                     sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUnitOfWork, UnitOfWork>(provider => new UnitOfWork(provider.GetRequiredService<T>()));

        return services;
    }

    private static IServiceCollection AddDI(this IServiceCollection services)
    {
        services
            .AddSingleton<SoftDeleteInterceptor>()
            .AddSingleton<UpdateAuditableEntitiesInterceptor>()
            .AddSingleton<PublishDomainEventsInterceptor>();
        return services;
    }
}