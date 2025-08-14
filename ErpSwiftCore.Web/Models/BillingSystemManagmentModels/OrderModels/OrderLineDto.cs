using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels
{
    /// <summary>
    /// تمثيل سطر الطلب
    /// </summary>
    public class OrderLineDto : AuditableEntityDto
    {
        public Guid OrderId { get; set; }
        public OrderDto Order { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
    }
}
