// <copyright file="TagIdTypeConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;
using UrlShortener.DomainCore.Identity;
public sealed class TagIdTypeConverter : StronglyTypedIdTypeConverter<string, TagId>;