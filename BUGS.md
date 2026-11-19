# BUGS

| Which rule from ASSIGNMENT.md is violated (R1–R8) | Where the problem was in the starter (file, approximate line) | How you discovered it (test, reading, AI) | What you changed |
|---|---|---|---|
| R5 | `src/RoomHub.Api/Services/BookingService.cs:19-38` | Reading the code: no check compares `startUtc` with the current time. | Added the check in `Booking.Create` and pass `TimeProvider.GetUtcNow()` from `CreateBookingHandler`. |
| R6 | `src/RoomHub.Api/Services/BookingService.cs:19-38` | Reading the code: nothing limits the duration. | Added the 4-hour check in `Booking.Create`; exactly 4 hours is allowed. |
| R7 | `src/RoomHub.Api/Services/BookingService.cs:34-38` | The loop checks every booking, including `Cancelled`, and uses `<=` on both sides, so touching bookings also conflict. I reproduced both with HTTP calls. | `EfBookingRepository.ListActiveForRoomAsync` filters `Active`, and `Booking.Overlaps` uses strict `<`. |
| R8 | `src/RoomHub.Api/Services/BookingService.cs:54-59` | Cancelling twice returned `204` because `Cancel` only assigned `Cancelled` again. | `Booking.Cancel()` now returns `Result` and a second cancel returns `409 Booking.AlreadyCancelled`. |
| R1–R4 (location, not behaviour) | `RoomService.cs` and `BookingService.cs` | Reading the code: the rules were in services, not in the entities. | Moved them into `Room.Create` and `Booking.Create`; the handlers only orchestrate. |
