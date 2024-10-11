using api.Contracts;
using api.Services;
using api.Utilities;
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
        var todoDtos = new PagedList<TodoDto>(
            todos.Page,
            todos.Size,
            todos.TotalCount,
            todos.Items
                .Select(TodoDto.FromTodo)
                .ToArray());

        return Results.Ok(todoDtos);
    }
}
