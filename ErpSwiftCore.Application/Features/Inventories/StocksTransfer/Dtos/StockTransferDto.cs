using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Dtos
{
    /// <summary>
    /// تمثيل مبسّط لعملية نقل مخزون
    /// </summary>
    public class StockTransferDto : AuditableEntityDto
    {
        public Guid ProductID { get; set; }
        public Guid FromWarehouseID { get; set; }
        public Guid ToWarehouseID { get; set; }
        public int Quantity { get; set; }
        public DateTime TransferDate { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? Notes { get; set; }
    }

    /// <summary>
    /// نقل مخزون مع تفاصيل المنتج
    /// </summary>
    public class StockTransferWithProductDto : StockTransferDto
    {
        public ProductDto Product { get; set; } = null!;
    }

    /// <summary>
    /// نقل مخزون مع تفاصيل المخازن (المرسَل والمنقول إليه)
    /// </summary>
    public class StockTransferWithWarehousesDto : StockTransferDto
    {
        public WarehouseDto FromWarehouse { get; set; } = null!;
        public WarehouseDto ToWarehouse { get; set; } = null!;
    }

    /// <summary>
    /// نقل مخزون مع كل التفاصيل (المنتج + المخازن + كليهما)
    /// </summary>
    public class StockTransferFullDetailDto : StockTransferDto
    {
        public ProductDto Product { get; set; } = null!;
        public WarehouseDto FromWarehouse { get; set; } = null!;
        public WarehouseDto ToWarehouse { get; set; } = null!;
    }

    /// <summary>
    /// ناتج عدد عمليات النقل
    /// </summary>
    public class StockTransferCountDto
    {
        public int TotalTransfers { get; set; }
    }

    /// <summary>
    /// ناتج عدد النقل لمنتج معيّن
    /// </summary>
    public class StockTransferCountByProductDto
    {
        public Guid ProductID { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// ناتج عدد النقل من/إلى مستودع معيّن
    /// </summary>
    public class StockTransferCountByWarehouseDto
    {
        public Guid WarehouseID { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// إجمالي الكمية المنقولة لمنتج في فترة زمنية
    /// </summary>
    public class TotalTransferredQuantityByProductDto
    {
        public Guid ProductID { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int TotalQuantity { get; set; }
    }

    /// <summary>
    /// إجمالي الكمية المنقولة من/إلى مستودع في فترة زمنية
    /// </summary>
    public class TotalTransferredQuantityByWarehouseDto
    {
        public Guid WarehouseID { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int TotalQuantity { get; set; }
    }

    /// <summary>
    /// نتيجة البحث في الـNotes أو المرجع
    /// </summary>
    public class StockTransferSearchResultsDto
    {
        public IReadOnlyList<StockTransferDto> Items { get; set; } = new List<StockTransferDto>();
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// نتيجة الصفحات (Paging) لعمليات النقل
    /// </summary>
    public class PagedStockTransferDto
    {
        public IReadOnlyList<StockTransferDto> Transfers { get; set; } = new List<StockTransferDto>();
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// آخر عملية نقل لمنتج معيّن
    /// </summary>
    public class LastStockTransferDto
    {
        public StockTransferDto? LastTransfer { get; set; }
    }

    /// <summary>
    /// بيانات إنشاء عملية نقل مخزون جديدة
    /// </summary>
    public class CreateStockTransferDto
    {
        /// <summary>معرف المنتج المنقول</summary>
        public Guid ProductID { get; set; }

        /// <summary>المستودع المرسَل منه</summary>
        public Guid FromWarehouseID { get; set; }

        /// <summary>المستودع المنقول إليه</summary>
        public Guid ToWarehouseID { get; set; }

        /// <summary>الكمية المنقولة</summary>
        public int Quantity { get; set; }

        /// <summary>تاريخ النقل</summary>
        public DateTime TransferDate { get; set; }

        /// <summary>رقم مرجعي اختياري</summary>
        public string? ReferenceNumber { get; set; }

        /// <summary>ملاحظات إضافية</summary>
        public string? Notes { get; set; }
    }

    /// <summary>
    /// نتيجة إنشاء عملية نقل مخزون (تعيد المعرف الجديد)
    /// </summary>
    public class CreateStockTransferResultDto
    {
        /// <summary>المعرف المنشأ حديثاً</summary>
        public Guid ID { get; set; }
    }

    /// <summary>
    /// بيانات تحديث عملية نقل مخزون
    /// </summary>
    public class UpdateStockTransferDto  
    {
        /// <summary>معرف العملية المراد تحديثها</summary>
        public Guid ID { get; set; }

        /// <summary>معرف المنتج المنقول</summary>
        public Guid ProductID { get; set; }

        /// <summary>المستودع المرسَل منه</summary>
        public Guid FromWarehouseID { get; set; }

        /// <summary>المستودع المنقول إليه</summary>
        public Guid ToWarehouseID { get; set; }

        /// <summary>الكمية المنقولة</summary>
        public int Quantity { get; set; }

        /// <summary>تاريخ النقل</summary>
        public DateTime TransferDate { get; set; }

        /// <summary>رقم مرجعي اختياري</summary>
        public string? ReferenceNumber { get; set; }

        /// <summary>ملاحظات إضافية</summary>
        public string? Notes { get; set; }
    }

    /// <summary>
    /// بيانات حذف عملية نقل مخزون
    /// </summary>
    public class DeleteStockTransferDto
    {
        /// <summary>معرف العملية المراد حذفها</summary>
        public Guid ID { get; set; }
    }

    /// <summary>
    /// بيانات حذف مجموعة من عمليات نقل المخزون
    /// </summary>
    public class DeleteStockTransfersRangeDto
    {
        /// <summary>قائمة المعرفات المراد حذفها</summary>
        public IEnumerable<Guid> IDs { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// أمر حذف جميع عمليات النقل (لا يحتوي على بيانات إضافية)</summary>
    public class DeleteAllStockTransfersDto { }

    /// <summary>
    /// بيانات استعادة عملية نقل مخزون محذوفة
    /// </summary>
    public class RestoreStockTransferDto
    {
        /// <summary>معرف العملية المراد استعادتها</summary>
        public Guid ID { get; set; }
    }

    /// <summary>
    /// بيانات استعادة مجموعة من عمليات نقل المخزون</summary>
    public class RestoreStockTransfersRangeDto
    {
        /// <summary>قائمة المعرفات المراد استعادتها</summary>
        public IEnumerable<Guid> IDs { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// أمر استعادة جميع عمليات النقل المحذوفة (لا يحتوي على بيانات إضافية)</summary>
    public class RestoreAllStockTransfersDto { }

    /// <summary>
    /// بيانات استيراد دفعي لعمليات نقل المخزون</summary>
    public class BulkImportStockTransfersDto
    {
        /// <summary>قائمة عمليات النقل المطلوب استيرادها</summary>
        public IEnumerable<CreateStockTransferDto> Transfers { get; set; } = new List<CreateStockTransferDto>();
    }

    /// <summary>
    /// نتيجة الاستيراد الدفعي (عدد العناصر المستوردة)</summary>
    public class BulkImportResultDto
    {
        /// <summary>عدد العمليات التي تم إنشاؤها</summary>
        public int ImportedCount { get; set; }
    }

    /// <summary>
    /// بيانات حذف دفعي لعمليات نقل المخزون</summary>
    public class BulkDeleteStockTransfersDto
    {
        /// <summary>قائمة المعرفات المراد حذفها</summary>
        public IEnumerable<Guid> IDs { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// نتيجة الحذف الدفعي (عدد العناصر المحذوفة)</summary>
    public class BulkDeleteResultDto
    {
        /// <summary>عدد العمليات التي تم حذفها</summary>
        public int DeletedCount { get; set; }
    }
    /// <summary>
    /// نتيجة التحقق من وجود عملية النقل
    /// </summary>
    public class StockTransferExistsDto
    {
        /// <summary>معرف عملية النقل المستعلم عنها</summary>
        public Guid TransferId { get; set; }
        /// <summary>هل العملية موجودة؟</summary>
        public bool Exists { get; set; }
    }

    /// <summary>
    /// نتيجة التحقق من وجود تحويل مكرر
    /// </summary>
    public class DuplicateTransferCheckDto
    {
        public Guid ProductId { get; set; }
        public Guid FromWarehouseId { get; set; }
        public Guid ToWarehouseId { get; set; }
        public DateTime TransferDate { get; set; }
        public Guid? ExcludeId { get; set; }
        public bool IsDuplicate { get; set; }
    }

    /// <summary>
    /// نتيجة التحقق من صحة المنتج
    /// </summary>
    public class ProductValidationDto
    {
        public Guid ProductId { get; set; }
        public bool IsValid { get; set; }
    }

    /// <summary>
    /// نتيجة التحقق من صحة المستودع
    /// </summary>
    public class WarehouseValidationDto
    {
        public Guid WarehouseId { get; set; }
        public bool IsValid { get; set; }
    }

    /// <summary>
    /// نتيجة التحقق من صحة الكمية
    /// </summary>
    public class QuantityValidationDto
    {
        public int Quantity { get; set; }
        public bool IsValid { get; set; }
    }
}