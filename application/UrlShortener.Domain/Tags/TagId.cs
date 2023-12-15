// <copyright file="TagId.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.Domain.Tags;

using System.ComponentModel;
using UrlShortener.DomainCore.Identity;
[TypeConverter(typeof(TagIdTypeConverter))]
public sealed record TagId(string Value) : StronglyTypedUlid<TagId>(Value);