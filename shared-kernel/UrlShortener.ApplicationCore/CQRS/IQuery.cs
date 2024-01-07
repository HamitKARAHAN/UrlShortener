// <copyright file="IQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediatR;

namespace UrlShortener.ApplicationCore.CQRS;
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
