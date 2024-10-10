namespace api.Models;
public class Todo
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime DueDate { get; private set; }
    public TodoStatus Status { get; private set; }
    public List<Tag> Tags { get; private set; } = [];
    public TodoPriority Priority { get; private set; }

    // For Entity Framework
    private Todo()
    {
    }

    public Todo(
        string title,
        DateTime createdAt,
        DateTime dueDate,
        TodoStatus status,
        List<Tag> tags,
        TodoPriority priority)
    {
        Id = Guid.NewGuid();
        Title = title;
        CreatedAt = createdAt;
        DueDate = dueDate;
        Status = status;
        Tags = tags;
        Priority = priority;
    }
}
