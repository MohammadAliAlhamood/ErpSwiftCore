using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompanyBranchs;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ErpSwiftCore.Web.Models.InventorySystemManagmentModels.WarehouseModels
{


    public class WarehouseViewModel
    {
        public WarehouseDto Warehouse { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }

    }

    /// <summary>
    /// تمثيل أساسي لبيانات المستودع
    /// </summary>
    public class WarehouseDto : BaseEntityDto
    {
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public Guid BranchID { get; set; }
        public CompanyBranchDto Branch { get; set; }
        public bool IsStorage { get; set; }
        public bool IsOperational { get; set; }
    }

    /// <summary>
    /// مستودع مع بيانات الفرع المرتبط
    /// </summary>
    public class WarehouseWithBranchDto : WarehouseDto
    {
        public CompanyBranchDto? Branch { get; set; }
    }

    /// <summary>
    /// مستودع مع لائحة المخزونات داخله
    /// </summary>
    public class WarehouseWithInventoriesDto : WarehouseDto
    {
        public IReadOnlyList<InventoryDto> Inventories { get; set; } = new List<InventoryDto>();
    }

    /// <summary>
    /// عدد المخزونات في مستودع واحد
    /// </summary>
    public class WarehouseInventoriesCountDto
    {
        public Guid WarehouseID { get; set; }
        public int TotalInventories { get; set; }
    }

    /// <summary>
    /// عدد المنتجات المختلفة في مستودع واحد
    /// </summary>
    public class WarehouseDistinctProductsCountDto
    {
        public Guid WarehouseID { get; set; }
        public int TotalDistinctProducts { get; set; }
    }

    /// <summary>
    /// عدد العناصر منخفضة المخزون في المستودع
    /// </summary>
    public class WarehouseLowStockCountDto
    {
        public Guid WarehouseID { get; set; }
        public int LowStockCount { get; set; }
    }

    /// <summary>
    /// عدد العناصر فائقة التخزين في المستودع
    /// </summary>
    public class WarehouseOverstockCountDto
    {
        public Guid WarehouseID { get; set; }
        public int OverstockCount { get; set; }
    }

    /// <summary>
    /// متوسط مستوى المخزون في المستودع
    /// </summary>
    public class WarehouseAverageStockLevelDto
    {
        public Guid WarehouseID { get; set; }
        public decimal AverageStockLevel { get; set; }
    }

    /// <summary>
    /// عدد المخازن الإجمالي أو حسب الفرع
    /// </summary>
    public class WarehouseCountDto
    {
        public int Count { get; set; }
    }

    /// <summary>
    /// إحصائية عدد المخزونات لكل مستودع
    /// </summary>
    public class InventoryCountPerWarehouseDto
    {
        public Guid WarehouseID { get; set; }
        public int InventoryCount { get; set; }
    } 
    public class RecentWarehouseDto : WarehouseDto
    {
        // إعادة استخدام خصائص WarehouseDto، يمكن إضافة حقول أخرى عند الحاجة
    } 
    public class CreateWarehouseDto
    {
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public Guid BranchID { get; set; }
        public bool IsStorage { get; set; }
        public bool IsOperational { get; set; }
    } 
    public class UpdateWarehouseDto : CreateWarehouseDto
    {
        public Guid ID { get; set; }
    }

    /// <summary>
    /// بيانات حذف مستودع واحد
    /// </summary>
    public class DeleteWarehouseDto
    {
        public Guid WarehouseId { get; set; }
    }

    /// <summary>
    /// بيانات حذف مجموعة مستودعات
    /// </summary>
    public class DeleteWarehousesRangeDto
    {
        public IEnumerable<Guid> WarehouseIds { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// أمر حذف جميع المستودعات
    /// </summary>
    public class DeleteAllWarehousesDto { }

    /// <summary>
    /// بيانات استرجاع مستودع واحد
    /// </summary>
    public class RestoreWarehouseDto
    {
        public Guid WarehouseId { get; set; }
    }

    /// <summary>
    /// بيانات استرجاع مجموعة مستودعات
    /// </summary>
    public class RestoreWarehousesRangeDto
    {
        public IEnumerable<Guid> WarehouseIds { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// أمر استرجاع جميع المستودعات
    /// </summary>
    public class RestoreAllWarehousesDto { }

    /// <summary>
    /// بيانات إضافة مجموعة مستودعات
    /// </summary>
    public class AddWarehousesRangeDto
    {
        public IEnumerable<CreateWarehouseDto> Warehouses { get; set; } = new List<CreateWarehouseDto>();
    }

    /// <summary>
    /// بيانات استيراد دفعي للمستودعات
    /// </summary>
    public class BulkImportWarehousesDto
    {
        public IEnumerable<CreateWarehouseDto> Warehouses { get; set; } = new List<CreateWarehouseDto>();
    }

    /// <summary>
    /// بيانات حذف دفعي للمستودعات وإرجاع عدد المحذوفات
    /// </summary>
    public class BulkDeleteWarehousesDto
    {
        public IEnumerable<Guid> WarehouseIds { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// بيانات للتحقق من وجود مستودع بمعرّف معين
    /// </summary>
    public class WarehouseExistsDto
    {
        public Guid WarehouseId { get; set; }
    }

    /// <summary>
    /// بيانات للتحقق من وجود اسم مستودع مكرر في فرع (مع إمكانية استثناء مستودع محدد)
    /// </summary>
    public class ExistsWarehouseNameDto
    {
        public string Name { get; set; } = null!;
        public Guid BranchId { get; set; }
        public Guid? ExcludeId { get; set; }
    }

    /// <summary>
    /// بيانات للتحقق من صلاحية الفرع المرتبط بالمستودع
    /// </summary>
    public class ValidateBranchDto
    {
        public Guid BranchId { get; set; }
    }
}