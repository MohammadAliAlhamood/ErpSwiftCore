using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels.Person;
namespace ErpSwiftCore.Web.Models.CRMSystemManagmentModels.CustomerModels
{
    public class CustomerDto : PersonDto
    {
        public string CustomerCode { get; set; } = null!;
        public decimal? CreditLimit { get; set; }
    }
}



