using RoomHub.Api.Common;

namespace RoomHub.Api.Domain;

public sealed class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Capacity { get; private set; }

    private Room() { }

    public static Result<Room> Create(string? name, int capacity)
    {
        var trimmed = name?.Trim();
        if (string.IsNullOrEmpty(trimmed))
            return Result<Room>.Failure(Error.Validation("Room.InvalidName", "Room name is required."));
        if (capacity <= 0)
            return Result<Room>.Failure(Error.Validation("Room.InvalidCapacity", "Capacity must be positive."));

        return Result<Room>.Success(new Room { Id = Guid.NewGuid(), Name = trimmed, Capacity = capacity });
    }
}
