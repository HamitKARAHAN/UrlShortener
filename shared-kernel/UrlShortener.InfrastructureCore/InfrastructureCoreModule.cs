// <copyright file="InfrastructureCoreModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.InfrastructureCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.InfrastructureCore.EntityFramework;
using UrlShortener.InfrastructureCore.Persistence;
public static class InfrastructureCoreModule
{
    public static void AddInfrastructureCoreModule<T>(this IServiceCollection services)
        where T : DbContext => services.ConfigureDatabaseContext<T>();

    private static IServiceCollection ConfigureDatabaseContext<T>(
        this IServiceCollection services)
        where T : DbContext
    {
        services
            .AddDbContext<T>((IServiceProvider sp, DbContextOptionsBuilder options)
             => options
                 .UseSqlServer("test")
                 .AddInterceptors(
                     sp.GetRequiredService<SoftDeleteInterceptor>(),
                     sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>(),
                     sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUnitOfWork, UnitOfWork>(provider => new UnitOfWork(provider.GetRequiredService<T>()));

        return services;
    }
}