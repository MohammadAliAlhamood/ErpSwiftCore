using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryPolicyModels
{
    /// <summary>
    /// تمثيل أساسي لسياسة المخزون
    /// </summary>
    public class InventoryPolicyDto : AuditableEntityDto
    {
        /// <summary>
        /// معرف المخزون المرتبط
        /// </summary>
        public Guid InventoryID { get; set; }
        public InventoryDto Inventory { get; set; } = null!;

        /// <summary>
        /// نوع السياسة (FIFO, LIFO, ...)
        /// </summary>
        public InventoryPolicyType PolicyType { get; set; }

        /// <summary>
        /// الحد الأدنى لإعادة الطلب
        /// </summary>
        public int ReorderLevel { get; set; }

        /// <summary>
        /// أقصى مستوى للمخزون
        /// </summary>
        public int MaxStockLevel { get; set; }

        /// <summary>
        /// تفعيل إعادة الطلب أوتوماتيكياً
        /// </summary>
        public bool AutoReorderEnabled { get; set; }

        /// <summary>
        /// كمية إعادة الطلب الأوتوماتيكي (إن وجدت)
        /// </summary>
        public int? AutoReorderQuantity { get; set; }

        /// <summary>
        /// ملاحظات إضافية
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// المستوى الحالي للمخزون (يمكن تعبئته من خارج)
        /// </summary>
        public int? CurrentStock { get; set; }
    }


    /// <summary>
    /// بيانات تفعيل إعادة الطلب التلقائي لسياسة مخزون
    /// </summary>
    public class EnableAutoReorderDto
    {
        /// <summary>
        /// معرف المخزون المرتبط بالسياسة
        /// </summary>
        public Guid InventoryId { get; set; }

        /// <summary>
        /// كمية إعادة الطلب الأوتوماتيكي
        /// </summary>
        public int ReorderQuantity { get; set; }
    }

    /// <summary>
    /// بيانات تعطيل إعادة الطلب التلقائي لسياسة مخزون
    /// </summary>
    public class DisableAutoReorderDto
    {
        /// <summary>
        /// معرف المخزون المرتبط بالسياسة
        /// </summary>
        public Guid InventoryId { get; set; }
    }

    /// <summary>
    /// بيانات تحديث كامل لسياسة مخزون
    /// </summary>
    public class UpdatePolicyDto : AuditableEntityDto
    { 
        public Guid InventoryID { get; set; }
        public InventoryPolicyType PolicyType { get; set; }
        public int ReorderLevel { get; set; }
        public int MaxStockLevel { get; set; }
        public bool AutoReorderEnabled { get; set; }
        public int? AutoReorderQuantity { get; set; }
        public string? Notes { get; set; }
    }

    /// <summary>
    /// بيانات تحديث حد إعادة الطلب لسياسة مخزون
    /// </summary>
    public class UpdateReorderLevelDto
    {
        /// <summary>
        /// معرف السياسة
        /// </summary>
        public Guid PolicyId { get; set; }

        /// <summary>
        /// الحد الأدنى لإعادة الطلب الجديد
        /// </summary>
        public int ReorderLevel { get; set; }
    }

    /// <summary>
    /// بيانات تحديث الحد الأقصى للمخزون لسياسة مخزون
    /// </summary>
    public class UpdateMaxStockLevelDto
    {
        /// <summary>
        /// معرف السياسة
        /// </summary>
        public Guid PolicyId { get; set; }

        /// <summary>
        /// الحد الأقصى للمخزون الجديد
        /// </summary>
        public int MaxStockLevel { get; set; }
    }

    /// <summary>
    /// بيانات تحديث عدة سياسات دفعة واحدة
    /// </summary>
    public class UpdatePoliciesRangeDto
    {
        /// <summary>
        /// قائمة السياسات مع القيم المحدثة
        /// </summary>
        public IEnumerable<UpdatePolicyDto> Policies { get; set; } = new List<UpdatePolicyDto>();
    }
}