using api.Models;

namespace api.Contracts;
public record TodoDto(
    Guid Id,
    string Title,
    DateTime CreatedAt,
    DateTime DueDate,
    TodoStatus Status,
    string[] Tags,
    TodoPriority Priority
){
    public static TodoDto FromTodo(Todo todo) => new(
        todo.Id,
        todo.Title,
        todo.CreatedAt,
        todo.DueDate,
        todo.Status,
        todo.Tags.Select(t => t.Name).ToArray(),
        todo.Priority
    );
}
