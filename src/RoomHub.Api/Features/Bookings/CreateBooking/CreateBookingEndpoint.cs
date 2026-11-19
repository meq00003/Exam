using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Bookings.CreateBooking;

public static class CreateBookingEndpoint
{
    public static RouteGroupBuilder MapCreateBookingEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/{roomId:guid}/bookings", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(
        Guid roomId, CreateBookingRequest request,
        ICommandHandler<CreateBookingCommand, CreateBookingResponse> handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateBookingCommand(roomId, request.BookedBy, request.StartUtc, request.EndUtc, request.Attendees);
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.IsSuccess
            ? TypedResults.Created($"/api/bookings/{result.Value.Id}", result.Value)
            : result.Error!.ToProblem();
    }
}

public sealed record CreateBookingRequest(string? BookedBy, DateTime StartUtc, DateTime EndUtc, int Attendees);
