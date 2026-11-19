using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Repositories;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public sealed class CancelBookingHandler(IBookingRepository bookings, IUnitOfWork unitOfWork)
    : ICommandHandler<CancelBookingCommand>
{
    public async Task<Result> HandleAsync(CancelBookingCommand command, CancellationToken cancellationToken = default)
    {
        var booking = await bookings.GetByIdAsync(command.Id, cancellationToken);
        if (booking is null)
            return Result.Failure(Error.NotFound("Booking.NotFound", "The booking does not exist."));

        var cancelled = booking.Cancel();
        if (!cancelled.IsSuccess) return cancelled;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
