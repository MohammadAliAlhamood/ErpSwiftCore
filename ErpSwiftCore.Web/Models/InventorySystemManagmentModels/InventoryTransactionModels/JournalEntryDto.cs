
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.JournalEntryModels;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryTransactionModels
{
   

    /// <summary>
    /// تمثيل كامل لحركة المخزون
    /// </summary>
    public class InventoryTransactionDto : AuditableEntityDto
    {
        /// <summary>
        /// معرف المخزون الذي تقع فيه الحركة
        /// </summary>
        public Guid InventoryID { get; set; }  
        public InventoryDto Inventory { get; set; } = null!;
        /// <summary>
        /// نوع الحركة (شراء، بيع، تعديل، ...)
        /// </summary>
        public InventoryTransactionType TransactionType { get; set; }

        /// <summary>
        /// تاريخ حدوث الحركة
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// الكمية المتغيرة في هذه الحركة
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// الرصيد الجاري بعد تطبيق الحركة
        /// </summary>
        public int RunningBalance { get; set; }

        /// <summary>
        /// رقم مرجعي اختياري (مثل رقم فاتورة، طلب شراء، ...)
        /// </summary>
        public string? ReferenceNumber { get; set; }

        /// <summary>
        /// ملاحظات إضافية على الحركة
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// معرف قيد اليومية المرتبط (إن وجد)
        /// </summary>
        public Guid? RelatedJournalEntryID { get; set; }

        /// <summary>
        /// بيانات قيد اليومية المرتبط (إن وجد)
        /// </summary>
        public JournalEntryDto? RelatedJournalEntry { get; set; }
    }

    /// <summary>
    /// لمخرجات تجميعية: عدد الحركات بحسب النوع أو النطاق الزمني
    /// </summary>
    public class InventoryTransactionSummaryDto
    {
        /// <summary>
        /// نوع الحركة
        /// </summary>
        public InventoryTransactionType TransactionType { get; set; }

        /// <summary>
        /// عدد الحركات لهذا النوع ضمن المعايير
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// لمخرجات تجميعية: إجمالي الكمية المتحركة لمنتج في فترة معينة
    /// </summary>
    public class InventoryTransactionAggregateDto
    {
        /// <summary>
        /// معرف المنتج
        /// </summary>
        public Guid ProductID { get; set; }

        /// <summary>
        /// إجمالي الكمية المتحركة (سواء زيادة أو نقصان) في الفترة
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// معدل دوران المخزون (Turnover Rate) في الفترة
        /// </summary>
        public decimal TurnoverRate { get; set; }
    }




   
   
     
}