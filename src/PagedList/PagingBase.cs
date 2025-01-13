using System.Text.Json;

namespace PagedList;

public abstract class PagingBase
{
    // If Page is set to 0, take all, ignore PageSize
    public int Page { get; set; } = 1;

    // If PageSize is set to 0, take all, ignore Page
    public int PageSize { get; set; } = 10;
    public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

    public (int Skip, int Take) Compute() {
        if (Page == 0 || PageSize == 0) {
            return (0, int.MaxValue);
        }

        // Ensure values are at least 1
        var safePage = Math.Max(1, Page);
        var safePageSize = Math.Max(1, PageSize);

        // Calculate skip and take values
        var skip = (safePage - 1) * safePageSize;
        var take = safePageSize;

        return (skip, take);
    }
    
    public PagingMetadata GetPagingInfo(int totalCount) {
        return new PagingMetadata() {
            Page = Page,
            PageSize = PageSize,
            TotalItemCount = totalCount,
            TotalPages = (int) Math.Ceiling(totalCount / (double) PageSize)
        };
    }

    public override string ToString() => JsonSerializer.Serialize(this);
}