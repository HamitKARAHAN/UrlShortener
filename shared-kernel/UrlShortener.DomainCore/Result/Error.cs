// <copyright file="Error.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Result;

using Ardalis.GuardClauses;

public sealed record Error
{
    private Error(string code, string message)
    {
        this.Code = code;
        this.Message = message;
    }

    public static readonly Error None = new (code: string.Empty, message: string.Empty);
    public static readonly Error NullValue = new (code: "Error.NullValue", message: "The specified result value is null");

    public string Code { get; }
    public string Message { get; }

    public static implicit operator string(Error error)
    {
        Guard.Against.Null(error);
        return error.Code;
    }

    public static Error Create(string code, string message)
    {
        Guard.Against.NullOrEmpty(code);
        Guard.Against.NullOrEmpty(message);
        return new Error(code: code, message: message);
    }
}