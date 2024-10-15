using api.Services;

namespace api.Extensions;
public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<TodoService>();
        services.AddScoped<TagService>();
        return services;
    }
}
