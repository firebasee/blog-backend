namespace Blog.SharedKernel;

public record PagedResult<T>(IEnumerable<T> Data, int Page, int PageSize, long TotalRecord)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalRecord / PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
    public IEnumerable<T> Data { get; set; } = Data;
}