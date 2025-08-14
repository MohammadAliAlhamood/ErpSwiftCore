using ErpSwiftCore.Domain.IServices;
namespace ErpSwiftCore.Persistence.Services
{
    public class PagedResult<T> : IPagedResult<T>
    {
        public PagedResult(IEnumerable<T> items, int totalCount)
        {
            Items = items.ToList().AsReadOnly();
            TotalCount = totalCount;
        }
        public int TotalCount { get; }
        public IReadOnlyList<T> Items { get; }
    }
}