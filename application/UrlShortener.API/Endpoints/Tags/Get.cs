// <copyright file="Get.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Net.Mime;
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
    ///       "https://localhost:5002/shortener/{shortCode}".
    ///
    /// </remarks>
    /// <response code="308">redirects to actual url.</response>
    /// <response code="400">when short code is not available.</response>
    [HttpGet("{shortCode}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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