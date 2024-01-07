// <copyright file="ICommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace UrlShortener.ApplicationCore.CQRS;
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}

public interface ICommand : IRequest
{
}