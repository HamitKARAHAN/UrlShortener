// <copyright file="TagDetailId.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.TagDetails;

using System.ComponentModel;
using UrlShortener.DomainCore.Identity;
[TypeConverter(typeof(TagDetailIdTypeConverter))]
public sealed record TagDetailId(string Value) : StronglyTypedUlid<TagDetailId>(Value);
