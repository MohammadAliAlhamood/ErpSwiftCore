using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.SharedKernel.Base;
using ErpSwiftCore.SharedKernel.Entities;

namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class Order : AuditableEntity
    {
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        public OrderType OrderType { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public decimal TotalAmount { get; set; }



        public Guid PartyId { get; set; }
        public Party Party { get; set; } = null!;



        public Guid CurrencyId { get; set; }
        public Currency  Currency { get; set; }

        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}