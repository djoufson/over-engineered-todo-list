namespace api.Utilities;
public class PagedList<T>(int page, int size, int totalCount, T[] items)
{
    public int Page { get; private set; } = page;
    public int Size { get; private set; } = size;
    public int TotalCount { get; private set; } = totalCount;
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)Size);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < Size;
    public T[] Items { get; private set; } = items;
}
