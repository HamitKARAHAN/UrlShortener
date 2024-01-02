// <copyright file="ValidationBehavior.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using Mediator;
using UrlShortener.DomainCore.Result;

namespace UrlShortener.ApplicationCore.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : ResultBase
{
    public async ValueTask<TResponse> Handle(TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        if (validators.Any())
        {
            return await next(message, cancellationToken);
        }

        return await next(message, cancellationToken);
    }
}
