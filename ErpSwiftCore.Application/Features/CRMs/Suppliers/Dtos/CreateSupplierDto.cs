using ErpSwiftCore.Application.Dtos.Person; 
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Dtos
{ 
    public class CreateSupplierDto : CreatePersonDto
    {
        public string SupplierCode { get; set; } = null!;
        public decimal? MaxSupplyLimit { get; set; }
    }

}
