namespace UrlShortener.InfrastructureCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.InfrastructureCore.EntityFramework;
using UrlShortener.InfrastructureCore.Persistence;

public static class InfrastructureCoreConfiguration
{
    public static void AddInfrastructureCoreModule<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext => services.ConfigureDatabaseContext<T>(configuration);

    private static IServiceCollection ConfigureDatabaseContext<T>(
        this IServiceCollection services,
        IConfiguration configuration) where T : DbContext
    {
        services
            .AddDbContext<T>((IServiceProvider sp, DbContextOptionsBuilder options) 
            => options
                .UseSqlServer("")
                .AddInterceptors(
                    sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>(), 
                    sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUnitOfWork, UnitOfWork>(provider => new UnitOfWork(provider.GetRequiredService<T>()));

        return services;
    }
}
