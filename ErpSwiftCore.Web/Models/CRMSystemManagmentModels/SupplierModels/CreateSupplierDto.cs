using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels.Person;
namespace ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels
{ 
    public class CreateSupplierDto : CreatePersonDto
    {
        public string SupplierCode { get; set; } = null!;
        public decimal? MaxSupplyLimit { get; set; }
    }
}
