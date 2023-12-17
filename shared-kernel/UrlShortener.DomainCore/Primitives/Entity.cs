// <copyright file="Entity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Primitives;

public abstract class Entity<T> : IEquatable<Entity<T>>
    where T : IComparable<T>
{
    protected Entity(T id) => this.Id = id;
    protected Entity() { }

    public T Id { get; private set; }

    /// <inheritdoc/>
    public virtual bool Equals(Entity<T> other) =>
        other is not null &&
        (ReferenceEquals(this, other) || EqualityComparer<T>.Default.Equals(this.Id, other.Id));

    /// <inheritdoc/>
    public override bool Equals(object obj) => this.Equals(obj as Entity<T>);

    public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(this.Id);

    public static bool operator ==(Entity<T> a, Entity<T> b) => a?.Equals(b) == true;

    public static bool operator !=(Entity<T> a, Entity<T> b) => !(a == b);
}
