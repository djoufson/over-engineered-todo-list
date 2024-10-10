using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Todos;
public class CreateTodo
{
    public static async Task<IResult> Handle(
        [FromBody] Request request,
        [FromServices] TodoService service,
        CancellationToken cancellationToken)
    {
        var todo = await service.CreateAsync(request, cancellationToken);
        return Results.Ok(todo);
    }

    public record Request(
        string Title,
        DateTime DueDate,
        string[] Tags,
        TodoPriority Priority
    );
}
