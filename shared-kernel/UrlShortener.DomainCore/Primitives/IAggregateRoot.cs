using UrlShortener.DomainCore.Abstractions;

namespace UrlShortener.DomainCore.Primitives;
public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
