using RoomHub.Api.Domain;
namespace RoomHub.Api.Features.Rooms.CreateRoom;
public sealed record CreateRoomResponse(Guid Id, string Name, int Capacity)
{
    public static CreateRoomResponse FromDomain(Room room) => new(room.Id, room.Name, room.Capacity);
}
