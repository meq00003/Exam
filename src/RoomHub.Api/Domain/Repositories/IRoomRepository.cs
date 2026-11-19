using RoomHub.Api.Domain;
namespace RoomHub.Api.Domain.Repositories;
public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Room room, CancellationToken cancellationToken = default);
}
