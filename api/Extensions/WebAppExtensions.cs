using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Extensions;
public static class WebAppExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
