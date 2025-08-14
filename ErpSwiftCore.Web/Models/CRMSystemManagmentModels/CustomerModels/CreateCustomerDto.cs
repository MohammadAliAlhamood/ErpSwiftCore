using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels.Person; 
namespace ErpSwiftCore.Web.Models.CRMSystemManagmentModels.CustomerModels
{ 
    public class CreateCustomerDto : CreatePersonDto
    {
        public string CustomerCode { get; set; } = null!;
        public decimal? CreditLimit { get; set; }
    }
}
