using RoomHub.Api.Domain;
namespace RoomHub.Api.Features.Rooms.GetRoom;
public sealed record GetRoomResponse(Guid Id, string Name, int Capacity)
{
    public static GetRoomResponse FromDomain(Room room) => new(room.Id, room.Name, room.Capacity);
}
