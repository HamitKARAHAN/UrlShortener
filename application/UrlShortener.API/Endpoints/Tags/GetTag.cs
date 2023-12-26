// <copyright file="GetTag.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.APICore.ApiResult;
using UrlShortener.DomainCore.Result;

namespace UrlShortener.API.Endpoints.Tags;

public sealed class GetTag(ISender sender)
    : EndpointBaseAsync
        .WithRequest<string>
        .WithResult<ApiResult>
{
    [HttpGet("{shortCode}")]
    public override async Task<ApiResult> HandleAsync(
        [FromRoute] string shortCode,
        CancellationToken cancellationToken = default)
    {
        Result<string> result = await sender.Send(request: new Application.Features.Tags.GetTag.GetTag.Query(shortCode), cancellationToken: cancellationToken);
        if (result.IsFailure)
        {
            return Result.BadRequest(result.Error);
        }

        return result.RedirectToUrl(result.Value);
    }
}