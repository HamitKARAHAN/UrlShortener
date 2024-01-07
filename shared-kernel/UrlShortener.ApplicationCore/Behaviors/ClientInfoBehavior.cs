// <copyright file="ClientInfoBehavior.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using UrlShortener.DomainCore.Signatures;

namespace UrlShortener.ApplicationCore.Behaviors;

public sealed class ClientInfoBehavior<TRequest>(IHttpContextAccessor httpContextAccessor) : IRequestPreProcessor<TRequest>
    where TRequest : notnull, IHasIpAddress
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        request.IPAddress = httpContextAccessor.HttpContext.Connection?.RemoteIpAddress?.ToString();
        await Task.CompletedTask;
    }
}
