namespace UrlShortener.DomainCore.Abstractions;
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
