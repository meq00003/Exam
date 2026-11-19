using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public static class CreateRoomEndpoint
{
    public static RouteGroupBuilder MapCreateRoomEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("", HandleAsync);
        return group;
    }

    private static async Task<IResult> HandleAsync(
        CreateRoomRequest request,
        ICommandHandler<CreateRoomCommand, CreateRoomResponse> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new CreateRoomCommand(request.Name, request.Capacity), cancellationToken);
        return result.IsSuccess
            ? TypedResults.Created($"/api/rooms/{result.Value.Id}", result.Value)
            : result.Error!.ToProblem();
    }
}

public sealed record CreateRoomRequest(string? Name, int Capacity);
