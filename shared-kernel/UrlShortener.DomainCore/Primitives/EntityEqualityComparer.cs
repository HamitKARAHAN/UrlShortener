// <copyright file="EntityEqualityComparer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Primitives;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
public sealed class EntityEqualityComparer<T> : IEqualityComparer<Entity<T>>
    where T : IComparable<T>
{
    public bool Equals(Entity<T> x, Entity<T> y) => ReferenceEquals(x, y) ||
               (x is not null &&
                y is not null &&
                EqualityComparer<T>.Default.Equals(x.Id, y.Id)
               );

    public int GetHashCode(Entity<T> obj)
    {
        Guard.Against.Null(obj);
        return EqualityComparer<T>.Default.GetHashCode(obj.Id);
    }
}