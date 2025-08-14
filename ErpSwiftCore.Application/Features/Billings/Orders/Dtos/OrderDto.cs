using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using ErpSwiftCore.Domain.Enums; 
namespace ErpSwiftCore.Application.Features.Billings.Orders.Dtos
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