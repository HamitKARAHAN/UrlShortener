// <copyright file="ClientInfoBehavior.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Mediator;
using Microsoft.AspNetCore.Http;
using UrlShortener.DomainCore.Result;
using UrlShortener.DomainCore.Signatures;

namespace UrlShortener.ApplicationCore.Behaviors;

public sealed class ClientInfoBehavior<TRequest, TResponse>(IHttpContextAccessor httpContextAccessor) : MessagePreProcessor<TRequest, TResponse>
    where TRequest : notnull, IMessage, IHasIpAddress
    where TResponse : ResultBase
{
    protected override async ValueTask Handle(TRequest message, CancellationToken cancellationToken)
    {
        message.IPAddress = httpContextAccessor.HttpContext.Connection?.RemoteIpAddress?.ToString();
        await ValueTask.CompletedTask;
    }
}
