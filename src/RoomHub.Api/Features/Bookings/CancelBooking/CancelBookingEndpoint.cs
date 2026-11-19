using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Bookings.CancelBooking;

public static class CancelBookingEndpoint
{
    public static RouteGroupBuilder MapCancelBookingEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/{id:guid}/cancel", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, ICommandHandler<CancelBookingCommand> handler, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new CancelBookingCommand(id), cancellationToken);
        return result.IsSuccess ? TypedResults.NoContent() : result.Error!.ToProblem();
    }
}
