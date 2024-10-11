using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Todos;
public class UnassignTag
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromBody] UnassignTagRequest request,
        [FromServices] TodoService service,
        CancellationToken cancellationToken
    )
    {
        var todo = await service.UnassignTagAsync(id, request.Tag, cancellationToken);
        if(todo is null)
        {
            return Results.NotFound("The task does not exist");
        }

        return Results.Ok(todo);
    }

    public record UnassignTagRequest(
        string Tag
    );
}
