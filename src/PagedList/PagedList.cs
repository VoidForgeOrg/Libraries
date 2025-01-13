namespace PagedList;

public class PagedList<T>
{
    public required PagingMetadata PagingMetadata { get; set; }
    public required List<T> Items { get; set; }
}