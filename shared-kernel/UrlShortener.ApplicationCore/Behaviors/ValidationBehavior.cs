// <copyright file="ValidationBehavior.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using UrlShortener.DomainCore.Signatures;

namespace UrlShortener.ApplicationCore.Behaviors;

public sealed class ValidationBehavior<TRequest>(IEnumerable<IValidator<TRequest>> validators) : IRequestPreProcessor<TRequest>
    where TRequest : IBaseRequest, IValidRequest
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            // TODOO throw
        }

        await Task.CompletedTask;
    }
}
