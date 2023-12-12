namespace UrlShortener.InfrastructureCore.Persistence;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UrlShortener.DomainCore.Abstractions;
using UrlShortener.DomainCore.Primitives;

public sealed class UnitOfWork(DbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        if (dbContext.ChangeTracker.Entries<IAggregateRoot>().Any(e => e.Entity.DomainEvents.Count != 0))
        {
            throw new InvalidOperationException("Domain events must be handled before committing the UnitOfWork.");
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
