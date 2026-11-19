using RoomHub.Api.Features.Bookings.CancelBooking;
using RoomHub.Api.Features.Bookings.CreateBooking;
using RoomHub.Api.Features.Bookings.GetBooking;

namespace RoomHub.Api.Features.Bookings;

public static class BookingEndpoints
{
    public static IEndpointRouteBuilder MapBookingEndpoints(this IEndpointRouteBuilder app)
    {
        var bookings = app.MapGroup("/api/bookings").WithTags("Bookings");
        bookings.MapGetBookingEndpoint();
        bookings.MapCancelBookingEndpoint();

        // POST /api/rooms/{roomId}/bookings
        var rooms = app.MapGroup("/api/rooms").WithTags("Bookings");
        rooms.MapCreateBookingEndpoint();
        return app;
    }
}
