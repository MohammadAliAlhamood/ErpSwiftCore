using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;
namespace ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots
{
    public class InventoryPolicy : AuditableEntity
    {
        public Guid InventoryID { get; set; }
        public Inventory Inventory { get; set; } = null!;
        public InventoryPolicyType PolicyType { get; set; } = InventoryPolicyType.FIFO;
        public int ReorderLevel { get; set; } = 10;
        public int MaxStockLevel { get; set; } = 1000;
        public bool AutoReorderEnabled { get; set; } = false;
        public int? AutoReorderQuantity { get; set; }
        public string? Notes { get; set; }
        public int? CurrentStock { get; set; }  
        public bool IsAutoReorderEnabled { get; set; }  
    }
}