using api.Data;
using api.Features.Todos;
using api.Models;
using api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace api.Services;
public class TodoService(TodoDbContext dbContext, TagService tagService)
{
    private readonly TodoDbContext _dbContext = dbContext;
    private readonly TagService _tagService = tagService;

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

    public async Task<Todo?> AssignTagAsync(Guid id, string tagName, CancellationToken cancellationToken)
    {
        var todo = await _dbContext.Tasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (todo == null)
            return null;

        var tag = await _tagService.CreateAsync(tagName.ToLower(), cancellationToken);
        todo.Tags.Add(tag);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return todo;
    }

    public async Task<Todo?> UnassignTagAsync(Guid id, string tagName, CancellationToken cancellationToken)
    {
        var todo = await _dbContext.Tasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (todo == null)
            return null;

        tagName = tagName.ToLower();
        todo.Tags.RemoveAll(t => t.Name == tagName);
        await _dbContext.SaveChangesAsync(cancellationToken);

        // If there is no longer a task associated with this tag, we completely delete it
        if(! await _dbContext.Tasks.AnyAsync(t => t.Tags.Any(tg => tg.Name == tagName), cancellationToken))
        {
            await _dbContext.Tags
                .Where(t => t.Name == tagName)
                .ExecuteDeleteAsync(cancellationToken);
        }
        return todo;
    }
}
