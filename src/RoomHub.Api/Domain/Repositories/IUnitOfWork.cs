namespace RoomHub.Api.Domain.Repositories;
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
