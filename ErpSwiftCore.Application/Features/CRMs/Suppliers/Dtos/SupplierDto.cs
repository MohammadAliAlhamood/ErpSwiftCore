using ErpSwiftCore.Application.Dtos.Person; 
namespace ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos
{ 
    public class SupplierDto : PersonDto
    {
        public string SupplierCode { get; set; } = null!;
        public decimal? MaxSupplyLimit { get; set; }
    }  
}