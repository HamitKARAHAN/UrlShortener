﻿// <copyright file="CreateTag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using UrlShortener.ApplicationCore.CQRS;
using UrlShortener.Domain.Abstractions;
using UrlShortener.Domain.Helpers;
using UrlShortener.Domain.Tags;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Extensions;
using UrlShortener.DomainCore.Result;
using UrlShortener.DomainCore.Signatures;

namespace UrlShortener.Application.Features.Tags.Create;

public static class CreateTag
{
    public sealed record Command(string Url, string Description, bool IsPublic) : ICommand<Result<CreateTagResponse>>,
        IHasIpAddress,
        IValidRequest
    {
        public string IPAddress { get; set; }
    }

    internal sealed class Handler(
        ITagRepository tagRepository,
        IUnitOfWork unitOfWork,
        IShortCodeGenerator shortCodeGenerator,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<Command, Result<CreateTagResponse>>
    {
        public async Task<Result<CreateTagResponse>> Handle(Command command, CancellationToken cancellationToken)
        {
            ShortCode shortCode = await tagRepository.GetShortCodeAsync(cacheKey: CacheKeyHelper.GetShortCodeByLongUrl(command.Url), command.Url, cancellationToken);
            if (shortCode is not null && !string.IsNullOrWhiteSpace(shortCode.Value))
            {
                return new CreateTagResponse(shortCodeGenerator.GenerateUrl(shortCode.Value));
            }

            Tag newTag = Tag
                .Create(
                    ShortCode.Create(shortCode: await shortCodeGenerator.GenerateShortCode(longUrl: command.Url)).Value,
                    LongUrl.Create(scheme: command.Url.GetScheme(), host: command.Url.GetHost()).Value,
                    Ip.Create(ipAddress: command.IPAddress).Value,
                    Description.Create(description: command.Description).Value,
                    isPublic: command.IsPublic,
                    dateTimeProvider.UtcNow())
                .Value;

            await tagRepository.AddAsync(newTag);
            await unitOfWork.SaveChangesAsync();
            return new CreateTagResponse(shortCodeGenerator.GenerateUrl(newTag.ShortCode));
        }
    }
}

public sealed class CreateTagValidator : AbstractValidator<CreateTag.Command>
{
    public CreateTagValidator()
    {
        this.RuleFor(x => x.Url).NotEmpty();
        this.RuleFor(x => x.Url).Matches(GuardExtensions.ValidUrl());
        this.RuleFor(x => x.Description).NotEmpty();
    }
}