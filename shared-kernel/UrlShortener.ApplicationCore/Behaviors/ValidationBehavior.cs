// <copyright file="ValidationBehavior.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using MediatR;
using UrlShortener.DomainCore.Result;

namespace UrlShortener.ApplicationCore.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : ResultBase
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            return await next();
        }

        return await next();
    }
}
