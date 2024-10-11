using api.Contracts;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Todos;
public class GetAllTodos
{
    public static async Task<IResult> Handle(
        [FromServices] TodoService service,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int size = 5)
    {
        var todos = await service.GetAllAsync(page, size, cancellationToken);
        var todoDtos = todos
            .Select(TodoDto.FromTodo);

        return Results.Ok(todoDtos);
    }
}
