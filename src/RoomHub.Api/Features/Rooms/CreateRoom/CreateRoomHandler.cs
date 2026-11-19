using RoomHub.Api.Common;
using RoomHub.Api.Common.Cqrs;
using RoomHub.Api.Domain;
using RoomHub.Api.Domain.Repositories;

namespace RoomHub.Api.Features.Rooms.CreateRoom;

public sealed class CreateRoomHandler(IRoomRepository rooms, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRoomCommand, CreateRoomResponse>
{
    public async Task<Result<CreateRoomResponse>> HandleAsync(CreateRoomCommand command, CancellationToken cancellationToken = default)
    {
        var created = Room.Create(command.Name, command.Capacity);
        if (!created.IsSuccess) return Result<CreateRoomResponse>.Failure(created.Error!);

        await rooms.AddAsync(created.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<CreateRoomResponse>.Success(CreateRoomResponse.FromDomain(created.Value));
    }
}
