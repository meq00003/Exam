# RoomHub (midterm starter)

Read `ASSIGNMENT.md` before anything else.

## Run

```bash
dotnet run --project src/RoomHub.Api
```

The application starts on `http://localhost:5090`. The test requests are in `roomhub.http`
(you can run them from Rider, or from VS Code with the REST Client extension).

## How to start

```bash
git clone <link-given-by-the-instructor> roomhub
cd roomhub
# create your own public repo and change the remote
git remote remove origin
git remote add origin <link-of-your-repo>
```

You can also create a new public repo and copy the contents of this folder into it.
What matters is that the repo you submit is **public** and contains your own commits.


## Architecture (exam version)

- `Features/Rooms`: `CreateRoom`, `GetRoom`.
- `Features/Bookings`: `CreateBooking`, `GetBooking`, `CancelBooking`.
- `Domain`: rich `Room` and `Booking` entities, rules R1-R8, repository interfaces.
- `Common`: `Result`, `Error`, CQRS interfaces, ProblemDetails mapping.
- `Infrastructure`: EF Core + SQLite (`roomhub.db`), repositories and unit of work.
- `TimeProvider` is injected; there is no `DateTime.UtcNow` in the business code.
