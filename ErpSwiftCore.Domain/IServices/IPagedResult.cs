namespace ErpSwiftCore.Domain.IServices
{
    public interface IPagedResult<T>
    {
        int TotalCount { get; }
        IReadOnlyList<T> Items { get; }
    }
}