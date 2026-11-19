using RoomHub.Api.Domain.Repositories;

namespace RoomHub.Api.Infrastructure;

public sealed class EfUnitOfWork(RoomHubDbContext db) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => db.SaveChangesAsync(cancellationToken);
}
