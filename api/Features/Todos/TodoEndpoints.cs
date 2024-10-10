namespace api.Features.Todos;
public static class TodoEndpoints
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/", CreateTodo.Handle);
        routes.MapGet("/", GetAllTodos.Handle);
        routes.MapGet("/{id:guid}", GetSingleTodo.Handle);
        routes.MapDelete("/{id:guid}", DeleteTodo.Handle);
        routes.MapPut("/{id:guid}", UpdateTodo.Handle);

        return routes;
    }
}
