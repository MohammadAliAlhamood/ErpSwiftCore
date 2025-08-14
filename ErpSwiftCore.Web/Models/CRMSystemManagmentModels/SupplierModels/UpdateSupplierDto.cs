using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels.Person;
namespace ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels
{ 
    public class UpdateSupplierDto : UpdatePersonDto
    {
        public string SupplierCode { get; set; } = null!;
        public decimal? MaxSupplyLimit { get; set; }
    }
}
