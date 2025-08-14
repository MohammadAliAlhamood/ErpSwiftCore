using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.SharedKernel.Base;
namespace ErpSwiftCore.Domain.Entities.EntityInventory
{
    public class Warehouse : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public Guid BranchID { get; set; }
        public CompanyBranch Branch { get; set; } = null!;
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public bool IsStorage { get; set; }
        public bool IsOperational { get; set; }
    }
}