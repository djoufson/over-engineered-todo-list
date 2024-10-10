using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Todos;
public class GetSingleTodo
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromServices] TodoService service,
        CancellationToken cancellationToken)
    {
        var todos = await service.GetAsync(id, cancellationToken);
        return Results.Ok(todos);
    }
}
