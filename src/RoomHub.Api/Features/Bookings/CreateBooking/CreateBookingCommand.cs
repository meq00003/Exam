namespace RoomHub.Api.Features.Bookings.CreateBooking;
public sealed record CreateBookingCommand(Guid RoomId, string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees);
