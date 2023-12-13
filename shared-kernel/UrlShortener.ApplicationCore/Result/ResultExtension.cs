// <copyright file="ResultExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore.Result;
using System.Net;
using Ardalis.GuardClauses;
public static class ResultExtension
{
    public static bool HasProblem(this ResultBase result)
    {
        Guard.Against.Null(result);
        return (int)result.StatusCode >= (int)HttpStatusCode.BadRequest;
    }
}
