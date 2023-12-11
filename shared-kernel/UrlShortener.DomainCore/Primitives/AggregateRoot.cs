namespace UrlShortener.DomainCore.Primitives;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UrlShortener.DomainCore.Abstractions;

public class AggregateRoot<T>(T id) : AuditableEntity<T>(id), IAggregateRoot where T : IComparable<T>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}