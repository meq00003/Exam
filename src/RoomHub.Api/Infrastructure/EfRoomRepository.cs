using Microsoft.EntityFrameworkCore;
using RoomHub.Api.Domain;
using RoomHub.Api.Domain.Repositories;

namespace RoomHub.Api.Infrastructure;

public sealed class EfRoomRepository(RoomHubDbContext db) : IRoomRepository
{
    public Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => db.Rooms.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(Room room, CancellationToken cancellationToken = default)
        => await db.Rooms.AddAsync(room, cancellationToken);
}
