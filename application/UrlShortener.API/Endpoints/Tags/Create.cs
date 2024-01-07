// <copyright file="Create.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    [HttpPost("api/shortenUrl")]
    public override async Task<ApiResult<CreateTagResponse>> HandleAsync([FromBody] CreateTagRequest request, CancellationToken cancellationToken = default)
        => await sender.Send(
            request: new CreateTag.Command(
                Url: request.Url,
                Description: request.Description,
                IsPublic: request.IsPublic),
            cancellationToken: cancellationToken);
}
