# AI-LOG

| Time (approx.) | What you asked (briefly) | What you kept | What you changed or rejected |
|---|---|---|---|
| 08:47 | Read `ASSIGNMENT.md`, `BUGS.md` and the starter, then propose the target structure. | The slice list (CreateRoom, GetRoom, CreateBooking, GetBooking, CancelBooking) and the EF Core plan. | I rejected keeping `Services/` and `ExceptionMiddleware`; business errors must be `Result`, not exceptions. |
| 08:50 | Generate the domain entities, `Result`/`Error` and the five slices. | `Room.Create`, `Booking.Create`, `Booking.Cancel`, the handlers and thin endpoints. | I moved the room lookup before validation in `CreateBookingHandler` so a missing room returns 404 first. |
| 08:53 | Add EF Core + SQLite with Repository and Unit of Work. | `RoomHubDbContext`, the two EF repositories and `EfUnitOfWork`. | I fixed the `using RoomHub.Api.Common` missing in four endpoints and pinned `SQLitePCLRaw.bundle_e_sqlite3` 2.1.12 because 2.1.11 had a known vulnerability. |
| 08:56 | Build and test the HTTP contract with `curl`. | The 201/404/409/204 behaviour. | I found that SQLite lost `DateTimeKind`, so `GetBookingResponse` now forces UTC before serializing. |
