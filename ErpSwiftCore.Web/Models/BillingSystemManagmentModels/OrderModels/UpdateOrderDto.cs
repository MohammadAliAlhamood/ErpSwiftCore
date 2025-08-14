using ErpSwiftCore.Web.Enums; 
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels
{
    /// <summary>
    /// بيانات تحديث الطلب (تغيير بيانات أساسية وإدارة الأسطر)
    /// </summary>
    public class UpdateOrderDto
    {
        public Guid Id { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public IEnumerable<CreateOrderLineDto>? LinesToAdd { get; set; }
        public IEnumerable<UpdateOrderLineDto>? LinesToUpdate { get; set; }
        public IEnumerable<Guid>? LineIdsToDelete { get; set; }
    }
    }


