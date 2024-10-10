using api.Data;
using api.Features.Todos;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;
public class TodoService(TodoDbContext dbContext)
{
    private readonly TodoDbContext _dbContext = dbContext;

    public async Task<Todo> CreateAsync(CreateTodo.Request request)
    {
        var createdAt = DateTime.UtcNow;
        var tags = request.Tags.Length > 0 ? _dbContext.Tags.Where(t => request.Tags.Contains(t.Name)).ToList() : [];
        var todo = new Todo(request.Title, createdAt, request.DueDate, TodoStatus.Pending, tags, request.Priority);
        _dbContext.Tasks.Add(todo);
        await _dbContext.SaveChangesAsync();
        return todo;
    }
}
