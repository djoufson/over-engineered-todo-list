using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;
public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<Todo> Tasks { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
