using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain;
using RoomHub.Api.Domain.Repositories;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public sealed class CreateBookingHandler(
    IRoomRepository rooms,
    IBookingRepository bookings,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider)
    : ICommandHandler<CreateBookingCommand, CreateBookingResponse>
{
    public async Task<Result<CreateBookingResponse>> HandleAsync(CreateBookingCommand command, CancellationToken cancellationToken = default)
    {
        // 404 first: the room must exist before any validation.
        var room = await rooms.GetByIdAsync(command.RoomId, cancellationToken);
        if (room is null)
            return Result<CreateBookingResponse>.Failure(Error.NotFound("Room.NotFound", "The room does not exist."));

        var created = Booking.Create(
            room.Id, command.BookedBy, command.StartUtc, command.EndUtc,
            command.Attendees, room.Capacity, timeProvider.GetUtcNow().UtcDateTime);
        if (!created.IsSuccess)
            return Result<CreateBookingResponse>.Failure(created.Error!);

        var existing = await bookings.ListActiveForRoomAsync(room.Id, cancellationToken);
        if (existing.Any(x => x.Overlaps(created.Value.StartUtc, created.Value.EndUtc)))
            return Result<CreateBookingResponse>.Failure(Error.Conflict("Booking.RoomOccupied", "The room is occupied in this interval."));

        await bookings.AddAsync(created.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<CreateBookingResponse>.Success(CreateBookingResponse.FromDomain(created.Value));
    }
}
