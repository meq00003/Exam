# DECISIONS

## 1. A rule you moved

I moved R3 (`endUtc` strictly after `startUtc`) and R6 (maximum 4 hours) from `Services/BookingService.cs` into `Domain/Booking.cs`, in `Booking.Create`. In the starter they were mixed with the HTTP-oriented service; now the entity cannot be created in an invalid state even if a new caller appears. The handler `Features/Bookings/CreateBooking/CreateBookingHandler.cs` only loads the room, calls the factory and saves.

## 2. Result instead of an exception

"The room is occupied" is an expected business outcome, not a programming failure. With `Result<CreateBookingResponse>` the handler returns `Error.Conflict("Booking.RoomOccupied", ...)` and the endpoint maps it to a `409` `ProblemDetails`; no middleware has to catch and reinterpret an exception. This also makes the success/failure path explicit in the type and easy to test.

## 3. Who knows about EF Core?

Only `Infrastructure/RoomHubDbContext.cs`, `EfRoomRepository`, `EfBookingRepository` and `EfUnitOfWork` know about EF Core. `Domain/Room.cs`, `Domain/Booking.cs`, the handlers and the endpoints depend on the interfaces in `Domain/Repositories/`. I gain testability and the ability to swap the database; I pay with more files and a mapping layer. The interfaces live in the domain because that is where the business code consumes them.

## 4. The flow of a request

`POST /api/rooms/{roomId}/bookings`
→ `Features/Bookings/CreateBooking/CreateBookingEndpoint.cs`
→ `ICommandHandler<CreateBookingCommand, CreateBookingResponse>` (DI)
→ `CreateBookingHandler.cs`
→ `IRoomRepository.GetByIdAsync` (404 if missing)
→ `Booking.Create(...)` (R2–R6)
→ `IBookingRepository.ListActiveForRoomAsync` + `Overlaps` (R7)
→ `IBookingRepository.AddAsync`
→ `IUnitOfWork.SaveChangesAsync`
→ `EfUnitOfWork` → `RoomHubDbContext` → SQLite (`roomhub.db`)
→ `201` with `{ id, roomId, status }` and `Location: /api/bookings/{id}`.

## 5. What you rejected from what the AI gave you

The first generated version kept the room-existence check after the validation of the booking fields. That contradicted the assignment ("404 before any validation"), so I moved the room lookup to the top of `CreateBookingHandler`. I also rejected a version that used `DateTime.UtcNow` in the handler and replaced it with the injected `TimeProvider`.

## 6. If you had 30 more minutes

I would add automated tests for R1–R8 and a real concurrency test for two simultaneous bookings of the same interval, because the current overlap check is a read-then-write and two requests could both pass it. I would also add a unique index or a transaction around the check, and clean up the duplicated `Room.NotFound` message constants.
