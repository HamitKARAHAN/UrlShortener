// <copyright file="CreateTag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UrlShortener.ApplicationCore.CQRS;
using UrlShortener.DomainCore.Result;

namespace UrlShortener.Application.Features.Tags;

public static class CreateTag
{
    public sealed record Command(string Url) : ICommand<Result<CreateTagResponse>>;

    public sealed class Handler : ICommandHandler<Command, Result<CreateTagResponse>>
    {
        public Task<Result<CreateTagResponse>> Handle(Command command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}