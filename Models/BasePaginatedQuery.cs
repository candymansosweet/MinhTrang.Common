public class BasePaginatedQuery
{
    public string Keyword { get; set; } = string.Empty;
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string SortField { get; init; } = string.Empty;
    public string SortOrder { get; init; } = string.Empty;
}