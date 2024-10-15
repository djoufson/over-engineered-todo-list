using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Tags;
public class CreateTag
{
    public static async Task<IResult> Handle(
        [FromBody] CreateTagRequest request,
        [FromServices] TagService service,
        CancellationToken cancellationToken)
    {
        var tag = await service.CreateAsync(request.Name, cancellationToken);
        return Results.Ok(tag);
    }

    public record CreateTagRequest(
        string Name
    );
}
