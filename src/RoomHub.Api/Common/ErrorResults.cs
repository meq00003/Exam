using Microsoft.AspNetCore.Http;

namespace RoomHub.Api.Common;

public static class ErrorResults
{
    public static IResult ToProblem(this Error error) => TypedResults.Problem(
        statusCode: error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        },
        title: error.Code,
        detail: error.Message);
}
