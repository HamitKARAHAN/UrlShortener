// <copyright file="ApiResultExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.APICore.ApiResult;

using Ardalis.GuardClauses;
using UrlShortener.ApplicationCore.Result;

public static class ApiResultExtension
{
    public static ApiResult RedirectToUrl<T>(this Result<T> result, string url)
    {
        Guard.Against.Null(result);
        Guard.Against.NullOrEmpty(url);
        return new ApiResult<T>(result, url);
    }
}
