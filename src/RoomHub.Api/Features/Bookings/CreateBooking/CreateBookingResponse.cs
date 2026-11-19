using RoomHub.Api.Domain;
namespace RoomHub.Api.Features.Bookings.CreateBooking;
public sealed record CreateBookingResponse(Guid Id, Guid RoomId, BookingStatus Status)
{
    public static CreateBookingResponse FromDomain(Booking booking) => new(booking.Id, booking.RoomId, booking.Status);
}
