using RoomHub.Api.Domain;
namespace RoomHub.Api.Features.Bookings.GetBooking;
public sealed record GetBookingResponse(Guid Id, Guid RoomId, string BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees, BookingStatus Status)
{
    public static GetBookingResponse FromDomain(Booking booking) => new(
        booking.Id, booking.RoomId, booking.BookedBy, AsUtc(booking.StartUtc), AsUtc(booking.EndUtc), booking.Attendees, booking.Status);

    private static DateTime AsUtc(DateTime value) => value.Kind == DateTimeKind.Utc ? value : DateTime.SpecifyKind(value, DateTimeKind.Utc);
}
