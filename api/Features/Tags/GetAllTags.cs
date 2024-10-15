using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Features.Tags;
public class GetAllTags
{
    public static async Task<IResult> Handle(
        [FromQuery] string? tagFilter,
        [FromQuery] string[]? excludedTags,
        [FromServices] TagService service,
        CancellationToken cancellationToken
    )
    {
        var tags = await service.GetAllAsync(tagFilter, excludedTags, cancellationToken);
        return Results.Ok(tags);
    }
}
