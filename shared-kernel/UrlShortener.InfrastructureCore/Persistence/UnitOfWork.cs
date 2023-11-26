namespace UrlShortener.InfrastructureCore.Persistence;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public sealed class UnitOfWork(DbContext dbContext)
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
