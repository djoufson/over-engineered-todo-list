using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Todos;
public class DeleteTodo
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromServices] TodoService service,
        CancellationToken cancellationToken)
    {
        var success = await service.DeleteAsync(id, cancellationToken);
        return Results.Ok(success);
    }
}
