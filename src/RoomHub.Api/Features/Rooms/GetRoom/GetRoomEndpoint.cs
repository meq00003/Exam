using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public static class GetRoomEndpoint
{
    public static RouteGroupBuilder MapGetRoomEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(Guid id, IQueryHandler<GetRoomQuery, GetRoomResponse> handler, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetRoomQuery(id), cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.Error!.ToProblem();
    }
}
