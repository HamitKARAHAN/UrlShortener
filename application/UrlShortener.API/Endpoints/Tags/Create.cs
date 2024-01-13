// <copyright file="Create.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Net.Mime;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.APICore.ApiResult;
using UrlShortener.Application.Features.Tags.Create;

namespace UrlShortener.API.Endpoints.Tags;

public sealed class Create(ISender sender)
    : EndpointBaseAsync
        .WithRequest<CreateTagRequest>
        .WithResult<ApiResult<CreateTagResponse>>
{
    /// <summary>
    /// Shortens Url and gives its shortened version.
    /// </summary>
    /// <param name="request">CreateTagRequest data.</param>
    /// <param name="cancellationToken">Cancellation Token for cancelling the request if needed.</param>
    /// <returns>Short Url for given Url.</returns>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     POST /CreateTagRequest
    ///     {
    ///        "url": "https://www.google.com",
    ///        "description": "¿Donde está mi mente?",
    ///        "isPublic": true
    ///     }.
    ///
    /// </remarks>
    /// <response code="200">returns shortened version of given url.</response>
    [HttpPost("shorten")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ApiResult<CreateTagResponse>> HandleAsync([FromBody] CreateTagRequest request, CancellationToken cancellationToken = default)
        => await sender.Send(
            request: new CreateTag.Command(
                Url: request.Url,
                Description: request.Description,
                IsPublic: request.IsPublic),
            cancellationToken: cancellationToken);
}
