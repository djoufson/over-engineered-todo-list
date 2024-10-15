using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Todos;
public class AssignTag
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromBody] AssignTagRequest request,
        [FromServices] TodoService service,
        CancellationToken cancellationToken
    )
    {
        var todo = await service.AssignTagAsync(id, request.Tag, cancellationToken);
        if (todo is null)
        {
            return Results.NotFound("The task does not exist");
        }

        return Results.Ok(todo);
    }

    public record AssignTagRequest(
        string Tag
    );
}
