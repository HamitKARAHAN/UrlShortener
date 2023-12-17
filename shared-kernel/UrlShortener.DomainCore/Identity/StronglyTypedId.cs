// <copyright file="StronglyTypedId.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Identity;

using Ardalis.GuardClauses;

public abstract record StronglyTypedId<TValue, T>(TValue Value) : IComparable<StronglyTypedId<TValue, T>>
    where T : StronglyTypedId<TValue, T>
    where TValue : IComparable<TValue>
{
    /// <inheritdoc/>
    public int CompareTo(StronglyTypedId<TValue, T> other) => other == null ? 1 : this.Value.CompareTo(other.Value);

    /// <inheritdoc/>
    public override int GetHashCode() => this.Value.GetHashCode();

    public abstract bool TryParse(TValue value, out T result);

    public virtual bool Equals(StronglyTypedId<TValue, T> other) => other != null && this.Value.Equals(other.Value);

    public static implicit operator TValue(StronglyTypedId<TValue, T> stronglyTypedId)
    {
        Guard.Against.Null(stronglyTypedId);
        return stronglyTypedId.Value;
    }
}
