// <copyright file="Create.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.APICore.ApiResult;
using UrlShortener.Application.Features.Tags;

namespace UrlShortener.API.Endpoints.Tags;

public sealed class Create(ISender sender)
    : EndpointBaseAsync
        .WithRequest<CreateTagRequest>
        .WithResult<ApiResult<CreateTagResponse>>
{
    [HttpPost("api/shortenUrl")]
    public override async Task<ApiResult<CreateTagResponse>> HandleAsync([FromBody] CreateTagRequest request, CancellationToken cancellationToken = default)
        => await sender.Send(request: new CreateTag.Command(request.Url), cancellationToken: cancellationToken);
}
