using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace PagedList;

public static class PagingExtensions
{
    public static T? ExtractPagingFromHeader<T>(this HttpRequest request) where T : PagingBase=>
        request.Headers.TryGetValue(PagingConstants.PaginationHeader, out var value)
            ? JsonSerializer.Deserialize<T>(value.Single() ?? "")
            : null;
    
    public static void AppendPagingHeaders(this HttpResponse response, PagingMetadata paging) => 
        response.Headers.Append(PagingConstants.PaginationHeader, JsonSerializer.Serialize(paging));
}