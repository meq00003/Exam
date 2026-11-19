using RoomHub.Api.Common;

namespace RoomHub.Api.Domain;

public sealed class Booking
{
    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string BookedBy { get; private set; } = string.Empty;
    public DateTime StartUtc { get; private set; }
    public DateTime EndUtc { get; private set; }
    public int Attendees { get; private set; }
    public BookingStatus Status { get; private set; }

    private Booking() { }

    public static Result<Booking> Create(
        Guid roomId, string? bookedBy, DateTime startUtc, DateTime endUtc,
        int attendees, int roomCapacity, DateTime nowUtc)
    {
        var trimmed = bookedBy?.Trim();
        if (string.IsNullOrEmpty(trimmed))
            return Result<Booking>.Failure(Error.Validation("Booking.BookedByRequired", "BookedBy is required."));

        startUtc = AsUtc(startUtc);
        endUtc = AsUtc(endUtc);
        nowUtc = AsUtc(nowUtc);

        if (endUtc <= startUtc)
            return Result<Booking>.Failure(Error.Validation("Booking.InvalidRange", "The end must be after the start."));
        if (attendees <= 0 || attendees > roomCapacity)
            return Result<Booking>.Failure(Error.Validation("Booking.InvalidAttendees", "Attendees must be positive and at most the room capacity."));
        if (startUtc < nowUtc)
            return Result<Booking>.Failure(Error.Validation("Booking.StartInPast", "A booking cannot start in the past."));
        if (endUtc - startUtc > TimeSpan.FromHours(4))
            return Result<Booking>.Failure(Error.Validation("Booking.TooLong", "A booking cannot last more than 4 hours."));

        return Result<Booking>.Success(new Booking
        {
            Id = Guid.NewGuid(), RoomId = roomId, BookedBy = trimmed,
            StartUtc = startUtc, EndUtc = endUtc, Attendees = attendees, Status = BookingStatus.Active
        });
    }

    public bool Overlaps(DateTime startUtc, DateTime endUtc)
        => Status == BookingStatus.Active && StartUtc < endUtc && startUtc < EndUtc;

    public Result Cancel()
        => Status == BookingStatus.Active
            ? CancelCore()
            : Result.Failure(Error.Conflict("Booking.AlreadyCancelled", "The booking is already cancelled."));

    private Result CancelCore() { Status = BookingStatus.Cancelled; return Result.Success(); }

    private static DateTime AsUtc(DateTime value) => value.Kind switch
    {
        DateTimeKind.Utc => value,
        DateTimeKind.Local => value.ToUniversalTime(),
        _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
    };
}
