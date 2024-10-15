using api.Features.Todos;

namespace api.Features.Tags;
public static class TagsEndpoints
{
    public static IEndpointRouteBuilder MapTagsEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/", CreateTag.Handle);
        routes.MapGet("/", GetAllTags.Handle);

        return routes;
    }
}
