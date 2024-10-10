namespace api.Features.Todos;
public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/todos", CreateTodo.Handle);
        routes.MapGet("/todos", GetAllTodos.Handle);
    }
}
