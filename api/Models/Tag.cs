namespace api.Models;

public class Tag
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    // For Entity Framework
    private Tag()
    {
    }

    public Tag(string name)
    {
        Name = name
            .Trim()
            .ToLower();
    }
}
