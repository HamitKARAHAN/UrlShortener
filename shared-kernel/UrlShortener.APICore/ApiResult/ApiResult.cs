// <copyright file="ApiResult.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.APICore.ApiResult;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UrlShortener.DomainCore.Result;

public partial class ApiResult(ResultBase result, string redirectUrl = null) : IResult
{
    protected string RedirectUrl { get;  } = redirectUrl;
    public Task ExecuteAsync(HttpContext httpContext) => this.ConvertToResult().ExecuteAsync(httpContext);

    public static string GetHttpStatusDisplayName(HttpStatusCode statusCode) => HttpStatusDisplayName().Replace(statusCode.ToString(), " $1");

    public static implicit operator ApiResult(Result result) => new (result);

    protected IResult GetProblemDetailsAsJson()
        => Results.Problem(
            title: GetHttpStatusDisplayName(result.StatusCode),
            statusCode: (int)result.StatusCode,
            detail: JsonConvert.SerializeObject(result.Error));

    protected virtual IResult ConvertToResult()
    {
        if (result.IsFailure && result.HasProblem())
        {
            return this.GetProblemDetailsAsJson();
        }

        return string.IsNullOrEmpty(this.RedirectUrl) ? Results.Ok() : Results.Redirect(this.RedirectUrl, true);
    }

    // <inheritdoc/>
    [GeneratedRegex("(?<=[a-z])([A-Z])")]
    private static partial Regex HttpStatusDisplayName();
}
