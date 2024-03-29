﻿// <copyright file="ApiResult{T}.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.APICore.ApiResult;

using Microsoft.AspNetCore.Http;
using UrlShortener.DomainCore.Result;

public class ApiResult<T>(Result<T> result, string redirectUrl = null) : ApiResult(result, redirectUrl)
{
    protected override IResult ConvertToResult()
    {
        if (result.IsFailure && result.HasProblem())
        {
            return this.GetProblemDetailsAsJson();
        }

        return string.IsNullOrEmpty(this.RedirectUrl) ? Results.Ok(result.Value) : Results.Redirect(this.RedirectUrl, true);
    }

    public static implicit operator ApiResult<T>(Result<T> result) => new (result);
}
