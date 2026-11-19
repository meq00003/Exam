using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Bookings.GetBooking;

public static class GetBookingEndpoint
{
    public static RouteGroupBuilder MapGetBookingEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, IQueryHandler<GetBookingQuery, GetBookingResponse> handler, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetBookingQuery(id), cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.Error!.ToProblem();
    }
}
