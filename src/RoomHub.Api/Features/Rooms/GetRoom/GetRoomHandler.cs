using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain.Repositories;

namespace RoomHub.Api.Features.Rooms.GetRoom;

public sealed class GetRoomHandler(IRoomRepository rooms) : IQueryHandler<GetRoomQuery, GetRoomResponse>
{
    public async Task<Result<GetRoomResponse>> HandleAsync(GetRoomQuery query, CancellationToken cancellationToken = default)
    {
        var room = await rooms.GetByIdAsync(query.Id, cancellationToken);
        return room is null
            ? Result<GetRoomResponse>.Failure(Error.NotFound("Room.NotFound", "The room does not exist."))
            : Result<GetRoomResponse>.Success(GetRoomResponse.FromDomain(room));
    }
}
