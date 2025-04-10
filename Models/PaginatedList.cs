using Microsoft.EntityFrameworkCore;
namespace Common.Models
{
    public class PaginatedList<T>
    {
        public IReadOnlyCollection<T> Items { get; }
        public long PageNumber { get; }
        public long TotalPages { get; }
        public long TotalCount { get; }

        public PaginatedList(IReadOnlyCollection<T> items, long totalCount, long pageNumber, long pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (long)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
            Items = items;
        }
    }
}
