using ErpSwiftCore.Domain.Abstractions;

namespace ErpSwiftCore.Domain.Entities.EntityCRM
{
    public class Supplier : Person
    {
        public string SupplierCode { get; set; } = string.Empty;
        public decimal? MaxSupplyLimit { get; set; }
    }
}