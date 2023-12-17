// <copyright file="AggregateRoot.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace UrlShortener.DomainCore.Primitives;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UrlShortener.DomainCore.Abstractions;

public class AggregateRoot<T> : AuditableEntity<T>, IAggregateRoot
    where T : IComparable<T>
{
    protected AggregateRoot(T id)
        : base(id)
    {
    }

    protected AggregateRoot() { }

    private readonly List<IDomainEvent> domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => this.domainEvents.AsReadOnly();

    public void ClearDomainEvents() => this.domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => this.domainEvents.Add(domainEvent);
}