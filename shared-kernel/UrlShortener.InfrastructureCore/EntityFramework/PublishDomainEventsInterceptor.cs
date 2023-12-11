namespace UrlShortener.InfrastructureCore.EntityFramework;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Primitives;

internal class PublishDomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PublishDomainEvents(eventData).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(eventData);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    } 

    private async Task PublishDomainEvents(DbContextEventData eventData)
    {
        Guard.Against.Null(eventData.Context);
        IEnumerable<IDomainEvent> domainEvents = eventData
                .Context
                .ChangeTracker
                .Entries<IAggregateRoot>()
                .Select(entry => entry.Entity)
                 .SelectMany(entity =>
                 {
                     IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;
                     entity.ClearDomainEvents();
                     return domainEvents;
                 })
                 .AsEnumerable();

            foreach (IDomainEvent domainEvent in domainEvents)
            {
                await publisher.Publish(domainEvent);
            }
    }
}
