// <copyright file="IQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore.CQRS;

using Mediator;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
