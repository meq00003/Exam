using RoomHub.Api.Domain;
namespace RoomHub.Api.Domain.Repositories;
public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Booking>> ListActiveForRoomAsync(Guid roomId, CancellationToken cancellationToken = default);
    Task AddAsync(Booking booking, CancellationToken cancellationToken = default);
}
