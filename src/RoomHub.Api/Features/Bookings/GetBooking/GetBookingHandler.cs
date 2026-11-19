using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Repositories;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public sealed class GetBookingHandler(IBookingRepository bookings) : IQueryHandler<GetBookingQuery, GetBookingResponse>
{
    public async Task<Result<GetBookingResponse>> HandleAsync(GetBookingQuery query, CancellationToken cancellationToken = default)
    {
        var booking = await bookings.GetByIdAsync(query.Id, cancellationToken);
        return booking is null
            ? Result<GetBookingResponse>.Failure(Error.NotFound("Booking.NotFound", "The booking does not exist."))
            : Result<GetBookingResponse>.Success(GetBookingResponse.FromDomain(booking));
    }
}
