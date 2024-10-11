using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;
public class TagService(TodoDbContext context)
{
    private readonly TodoDbContext _context = context;

    public async Task<Tag> CreateAsync(string name, CancellationToken cancellationToken)
    {
        var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
        if (existingTag != null)
        {
            return existingTag;
        }

        var tag = new Tag(name);
        await _context.Tags.AddAsync(tag, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return tag;
    }

    public async Task<Tag[]> GetAllAsync(string? tagFilter, string[]? excludedTags, CancellationToken cancellationToken)
    {
        var query = _context.Tags.AsQueryable();
        if(!string.IsNullOrEmpty(tagFilter))
        {
            query = query.Where(t => t.Name.Contains(tagFilter.Trim().ToLower()));
        }

        if(excludedTags != null && excludedTags.Length > 0)
        {
            query = query.Where(t => !excludedTags.Contains(t.Name.Trim().ToLower()));
        }

        return await query.ToArrayAsync(cancellationToken);
    }
}
