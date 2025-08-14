using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
namespace ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels
{ 
    public class OrderDto : AuditableEntityDto
    {
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus OrderStatus { get; set; }

        // ربط بالطرف الموحد
        public Guid PartyId { get; set; }
        public PartyDto Party { get; set; } = null!;

        public decimal TotalAmount { get; set; }
        public Guid CurrencyId { get; set; }
        public CurrencyDto Currency { get; set; }
        public ICollection<OrderLineDto> OrderLines { get; set; } = new List<OrderLineDto>();
     }
}