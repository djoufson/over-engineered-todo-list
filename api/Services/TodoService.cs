using api.Data;
using api.Features.Todos;
using api.Models;
using api.Utilities;
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

    public async Task<PagedList<Todo>> GetAllAsync(int page, int size, CancellationToken cancellationToken)
    {
        var query = _dbContext.Tasks
            .Include(t => t.Tags)
            .OrderByDescending(t => t.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var data = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToArrayAsync(cancellationToken);

        return new PagedList<Todo>(page, size, totalCount, data);
    }

    public async Task<Todo?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync(cancellationToken) > 0;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateTodo.UpdateTodoRequest request, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Title, request.Title)
                .SetProperty(t => t.DueDate, request.DueDate)
                .SetProperty(t => t.Priority, request.Priority)
            , cancellationToken) > 0;
    }
}
