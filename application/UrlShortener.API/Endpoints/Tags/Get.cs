// <copyright file="Get.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.APICore.ApiResult;
using UrlShortener.Application.Features.Tags.GetTag;
using UrlShortener.DomainCore.Result;

namespace UrlShortener.API.Endpoints.Tags;

public sealed class Get(ISender sender)
    : EndpointBaseAsync
        .WithRequest<string>
        .WithResult<ApiResult>
{
    /// <summary>
    /// Shortens Url and gives its shortened version.
    /// </summary>
    /// <param name="shortCode">Short Code for created for an actual url.</param>
    /// <param name="cancellationToken">Cancellation Token for cancelling the request if needed.</param>
    /// <returns>Redirects user to actual shortened url.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///       "https://localhost:5002/api/{shortCode}".
    ///
    /// </remarks>
    /// <response code="200">returns shortened version of given url.</response>
    /// <response code="400">when short code not available.</response>
    [HttpGet("api/{shortCode}")]
    public override async Task<ApiResult> HandleAsync(
        [FromRoute] string shortCode,
        CancellationToken cancellationToken = default)
    {
        Result<string> result = await sender.Send(request: new GetTag.Query(shortCode), cancellationToken: cancellationToken);
        if (result.IsFailure)
        {
            return Result.BadRequest(result.Error);
        }

        return result.RedirectToUrl(result.Value);
    }
}