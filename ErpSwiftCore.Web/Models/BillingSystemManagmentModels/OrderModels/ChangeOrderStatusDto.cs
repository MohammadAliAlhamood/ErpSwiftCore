using ErpSwiftCore.Web.Enums; 

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels
{ 
    public class ChangeOrderStatusDto
    {
        public Guid OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }
}
