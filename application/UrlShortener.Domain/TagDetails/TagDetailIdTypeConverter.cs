// <copyright file="TagDetailIdTypeConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.TagDetails;
using UrlShortener.DomainCore.Identity;
public sealed class TagDetailIdTypeConverter : StronglyTypedIdTypeConverter<string, TagDetailId>;