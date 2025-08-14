using ErpSwiftCore.Domain.Abstractions;

namespace ErpSwiftCore.Domain.Entities.EntityCRM
{
    public class Customer : Person
    {
        public string CustomerCode { get; set; } = string.Empty;
        public decimal? CreditLimit { get; set; }
    }
}