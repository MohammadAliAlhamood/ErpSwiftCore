using ErpSwiftCore.SharedKernel.Base;
namespace ErpSwiftCore.SharedKernel.Entities
{
    public class Currency : BaseEntity
    { 
        public string? CurrencyCode { get; set; } 
        public string? CurrencyName { get; set; }
    }
}