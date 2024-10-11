using api.Contracts;
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
        var todo = await service.GetAsync(id, cancellationToken);
        if(todo is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(TodoDto.FromTodo(todo));
    }
}
