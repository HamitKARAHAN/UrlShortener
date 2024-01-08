// <copyright file="UnitOfWork.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.InfrastructureCore.Persistence;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Primitives;

public sealed class UnitOfWork(DbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);
}
