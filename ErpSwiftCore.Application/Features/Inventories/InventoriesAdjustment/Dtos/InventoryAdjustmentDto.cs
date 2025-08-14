using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Dtos
{


    /// <summary>
    /// DTO لتمثيل تعديل المخزون مع تضمين بيانات المنتج والمستودع
    /// </summary>
    public class InventoryAdjustmentDto : AuditableEntityDto
    {
        public Guid ProductID { get; set; }
        public ProductDto? Product { get; set; }

        public Guid WarehouseID { get; set; }
        public WarehouseDto? Warehouse { get; set; }

        public int QuantityChanged { get; set; }
        public string Reason { get; set; } = null!;
        public DateTime AdjustmentDate { get; set; }
    }

    /// <summary>
    /// DTO لتمثيل نتائج إحصائية: عدد التعديلات مجمّعاً حسب السبب
    /// </summary>
    public class AdjustmentReasonCountDto
    {
        public string Reason { get; set; } = null!;
        public int Count { get; set; }
    }
    /// <summary>
    /// بيانات إنشاء تعديل يدوي
    /// </summary>
    public class CreateInventoryAdjustmentDto
    {
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public int QuantityChanged { get; set; }
        public string Reason { get; set; } = null!;
    }

    /// <summary>
    /// بيانات حذف تعديل واحد
    /// </summary>
    public class DeleteInventoryAdjustmentDto
    {
        public Guid AdjustmentId { get; set; }
    }

    /// <summary>
    /// بيانات استرجاع تعديل واحد
    /// </summary>
    public class RestoreInventoryAdjustmentDto
    {
        public Guid AdjustmentId { get; set; }
    }

    /// <summary>
    /// بيانات حذف مجموعة تعديلات
    /// </summary>
    public class DeleteInventoryAdjustmentsRangeDto
    {
        public IEnumerable<Guid> AdjustmentIds { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// أمر حذف جميع التعديلات (لا حاجة لبيانات إضافية)
    /// </summary>
    public class DeleteAllInventoryAdjustmentsDto { }

    /// <summary>
    /// بيانات استرجاع مجموعة تعديلات
    /// </summary>
    public class RestoreInventoryAdjustmentsRangeDto
    {
        public IEnumerable<Guid> AdjustmentIds { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// أمر استرجاع جميع التعديلات المحذوفة نرمياً
    /// </summary>
    public class RestoreAllInventoryAdjustmentsDto { }

    /// <summary>
    /// بيانات حذف دفعي مع إرجاع عدد المحذوفات
    /// </summary>
    public class BulkDeleteInventoryAdjustmentsDto
    {
        public IEnumerable<Guid> AdjustmentIds { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// بيانات تعديل سجل تعديل موجود (للطبقة الدمجية في UpdateAdjustmentsAsync)
    /// </summary>
    public class UpdateInventoryAdjustmentDto
    {
        public Guid Id { get; set; }
        public int QuantityChanged { get; set; }
        public string Reason { get; set; } = null!;
        public DateTime AdjustmentDate { get; set; }
    }

    /// <summary>
    /// بيانات تحديث دفعة من التعديلات
    /// </summary>
    public class UpdateInventoryAdjustmentsRangeDto
    {
        public IEnumerable<UpdateInventoryAdjustmentDto> Adjustments { get; set; }
            = new List<UpdateInventoryAdjustmentDto>();
    }

    /// <summary>
    /// بيانات تغيير سبب التعديل ضمن نطاق زمني
    /// </summary>
    public class UpdateInventoryAdjustmentReasonByDateRangeDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string NewReason { get; set; } = null!;

        /// <summary>
        /// بيانات للتحقق من وجود تعديل بمُعرّف معين
        /// </summary>
        public class AdjustmentExistsDto
        {
            public Guid AdjustmentId { get; set; }
        }

        /// <summary>
        /// بيانات للتحقق من وجود تعديل لمنتج ومستودع في تاريخ معين،
        /// مع إمكانية استثناء تعديل محدّد
        /// </summary>
        public class ExistsForProductWarehouseOnDateDto
        {
            public Guid ProductId { get; set; }
            public Guid WarehouseId { get; set; }
            public DateTime Date { get; set; }
            public Guid? ExcludeId { get; set; }
        }

        /// <summary>
        /// بيانات للتحقق من صلاحية المعرّف الخاص بالمنتج
        /// </summary>
        public class ValidateProductDto
        {
            public Guid ProductId { get; set; }
        }

        /// <summary>
        /// بيانات للتحقق من صلاحية المعرّف الخاص بالمستودع
        /// </summary>
        public class ValidateWarehouseDto
        {
            public Guid WarehouseId { get; set; }
        }

        /// <summary>
        /// بيانات للتحقق من صلاحية الكمية (غير صفرية)
        /// </summary>
        public class ValidateQuantityDto
        {
            public int Quantity { get; set; }
        }
    }
}