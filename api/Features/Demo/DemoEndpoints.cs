using api.Features.Tags;

namespace api.Features.Demo;
public static class DemoEndpoints
{
    public static IEndpointRouteBuilder MapDemoEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/", () => "Hello Demo");
        return routes;
    }
}
