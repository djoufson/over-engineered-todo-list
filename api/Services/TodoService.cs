using api.Data;
using api.Features.Todos;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;
public class TodoService(TodoDbContext dbContext)
{
    private readonly TodoDbContext _dbContext = dbContext;

    public async Task<Todo> CreateAsync(CreateTodo.Request request, CancellationToken cancellationToken)
    {
        var createdAt = DateTime.UtcNow;
        var tags = request.Tags.Length > 0 ? _dbContext.Tags.Where(t => request.Tags.Contains(t.Name)).ToList() : [];
        var todo = new Todo(request.Title, createdAt, request.DueDate, TodoStatus.Pending, tags, request.Priority);
        _dbContext.Tasks.Add(todo);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return todo;
    }

    public async Task<Todo[]> GetAllAsync(int page, int size, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .Include(t => t.Tags)
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .ToArrayAsync(cancellationToken);
    }
}
