// <copyright file="DomainErrors.TagErrors.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using UrlShortener.DomainCore.Result;

namespace UrlShortener.Domain.Tags;

public static partial class DomainErrors
{
    public static class TagErrors
    {
        public static readonly Error Error1 = Error.Create(code: "ERROR1", message: "ERROR1");
        public static readonly Error Error2 = Error.Create(code: "ERROR2", message: "ERROR2");
    }
}
