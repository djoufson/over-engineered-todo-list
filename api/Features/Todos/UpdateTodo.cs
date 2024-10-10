using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Todos;
public class UpdateTodo
{
    public static async Task<IResult> Handle(
        [FromRoute] Guid id,
        [FromBody] UpdateTodoRequest request,
        [FromServices] TodoService service,
        CancellationToken cancellationToken)
    {
        var success = await service.UpdateAsync(id, request, cancellationToken);
        return Results.Ok(success);
    }

    public record UpdateTodoRequest(
        string Title,
        DateTime DueDate,
        TodoPriority Priority
    );
}
