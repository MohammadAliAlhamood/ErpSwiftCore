using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.Inventories.Dtos
{

    /// <summary>
    /// تمثيل مبسّط لمخزون داخل مستودع
    /// </summary>
    public class InventoryDto 

    {
        public Guid ID { get; set; }
        public Guid ProductID { get; set; }
        public Guid WarehouseID { get; set; }
        public int QuantityOnHand { get; set; }
        public int QuantityReserved { get; set; }
    }
    /// <summary>
    /// مخزون متضمّن سياسته
    /// </summary>
    public class InventoryWithPolicyDto : InventoryDto
    {
        public InventoryPolicyDto Policy { get; set; } = null!;
    }

    /// <summary>
    /// مخزون متضمّن الحركات (Transactions)
    /// </summary>
    public class InventoryWithTransactionsDto : InventoryDto
    {
        public IReadOnlyList<InventoryTransactionDto> Transactions { get; set; } = new List<InventoryTransactionDto>();
    }

    /// <summary>
    /// مخزون متضمّن الإشعارات (Notifications)
    /// </summary>
    public class InventoryWithNotificationsDto : InventoryDto
    {
        public IReadOnlyList<NotificationDto> Notifications { get; set; } = new List<NotificationDto>();
    }

    /// <summary>
    /// مخزون متضمّن كل التفاصيل (Product + Warehouse + Policy + Transactions + Notifications)
    /// </summary>
    public class InventoryFullDetailDto : InventoryDto
    {
        public InventoryPolicyDto Policy { get; set; } = null!;
        public IReadOnlyList<InventoryTransactionDto> Transactions { get; set; } = new List<InventoryTransactionDto>();
        public IReadOnlyList<NotificationDto> Notifications { get; set; } = new List<NotificationDto>();
    }


    /// <summary>
    /// ناتج العدّ العام للمخزونات
    /// </summary>
    public class InventoryCountDto
    {
        public int TotalInventories { get; set; }
    }

    /// <summary>
    /// ناتج عدّ المخزونات لمنتج معيّن
    /// </summary>
    public class InventoryCountByProductDto
    {
        public Guid ProductID { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// ناتج عدّ المخزونات في مستودع معيّن
    /// </summary>
    public class InventoryCountByWarehouseDto
    {
        public Guid WarehouseID { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// ناتج عدّ العناصر دون المخزون المتاح (Under low stock)
    /// </summary>
    public class LowStockCountDto
    {
        public Guid WarehouseID { get; set; }
        public int CountBelowReorder { get; set; }
    }

    /// <summary>
    /// ناتج عدّ العناصر فوق السعة القصوى (Overstock)
    /// </summary>
    public class OverstockCountDto
    {
        public Guid WarehouseID { get; set; }
        public int CountAboveMax { get; set; }
    }

    /// <summary>
    /// توافر منتج واحد عبر المستودعات
    /// </summary>
    public class ProductAvailabilityDto
    {
        public Guid ProductID { get; set; }
        public IReadOnlyDictionary<Guid, int> QuantityByWarehouse { get; set; } = new Dictionary<Guid, int>();
    }

    /// <summary>
    /// ملخص المخزون لعدة منتجات
    /// </summary>
    public class StockSummaryByProductDto
    {
        public IReadOnlyDictionary<Guid, int> TotalAvailableByProduct { get; set; } = new Dictionary<Guid, int>();
    }

    /// <summary>
    /// إجمالي الكمية المتاحة في مستودع
    /// </summary>
    public class TotalAvailableQuantityDto
    {
        public Guid WarehouseID { get; set; }
        public int TotalAvailable { get; set; }
    }

    /// <summary>
    /// إجمالي الكمية المحجوزة في مستودع
    /// </summary>
    public class TotalReservedQuantityDto
    {
        public Guid WarehouseID { get; set; }
        public int TotalReserved { get; set; }
    }

    /// <summary>
    /// متوسط مستوى المخزون في مستودع
    /// </summary>
    public class AverageStockLevelDto
    {
        public Guid WarehouseID { get; set; }
        public decimal AverageOnHand { get; set; }
    }

    /// <summary>
    /// قيمة المخزون في مستودع (Pending: Requires product pricing)
    /// </summary>
    public class InventoryValueDto
    {
        public Guid WarehouseID { get; set; }
        public decimal TotalValue { get; set; }
    }

    /// <summary>
    /// معدل دوران المخزون لمنتج في فترة زمنية
    /// </summary>
    public class TurnoverRateDto
    {
        public Guid ProductID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal Rate { get; set; }
    }

    /// <summary>
    /// المخزون الحالي بعد كل التعديلات لمنتج في مستودع
    /// </summary>
    public class CurrentStockAfterAdjustmentsDto
    {
        public Guid ProductID { get; set; }
        public Guid WarehouseID { get; set; }
        public int CurrentStock { get; set; }
    }

    /// <summary>
    /// قائمة المخزونات التي دون مستوى إعادة الطلب
    /// </summary>
    public class BelowReorderLevelListDto
    {
        public IReadOnlyList<InventoryDto> Items { get; set; } = new List<InventoryDto>();
    }

    /// <summary>
    /// قائمة المخزونات التي فوق المستوى الأقصى
    /// </summary>
    public class AboveMaxStockLevelListDto
    {
        public IReadOnlyList<InventoryDto> Items { get; set; } = new List<InventoryDto>();
    }

    
}