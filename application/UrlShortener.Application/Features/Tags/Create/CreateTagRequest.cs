// <copyright file="CreateTagRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace UrlShortener.Application.Features.Tags.Create;

public record CreateTagRequest(
    [JsonProperty("url")] string Url,
    [JsonProperty(propertyName: "description")] string Description,
    [JsonProperty(propertyName: "isPublic")] bool IsPublic);
