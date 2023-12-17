// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UrlShortener.APICore;
using UrlShortener.Application;
using UrlShortener.Infrastructure;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApiCoreModule()
    .AddApplicationModule()
    .AddInfrastructureModule(configuration: builder.Configuration);
WebApplication app = builder.Build();
app.UseHttpsRedirection();
app.Run();