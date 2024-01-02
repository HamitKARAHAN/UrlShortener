// <copyright file="ApplicationCoreModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore;

using FluentValidation;
using Mediator;
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
        services.AddMediator();
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ClientInfoBehavior<,>));
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
