// <copyright file="IQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.ApplicationCore.CQRS;
using MediatR;
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
