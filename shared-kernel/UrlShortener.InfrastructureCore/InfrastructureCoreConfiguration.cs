namespace UrlShortener.InfrastructureCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class InfrastructureCoreConfiguration
{
    public static void AddInfrastructureCoreModule<T>(this IServiceCollection services, Assembly assembly) where T : DbContext
    {

    }
}
