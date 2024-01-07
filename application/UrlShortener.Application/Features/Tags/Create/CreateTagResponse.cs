// <copyright file="CreateTagResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace UrlShortener.Application.Features.Tags.Create;

public record CreateTagResponse([JsonProperty("shortUrl")]string ShortUrl);
