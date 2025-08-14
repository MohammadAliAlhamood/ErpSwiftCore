using ErpSwiftCore.Application.Dtos.Person; 
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Dtos
{ 
    public class CreateCustomerDto : CreatePersonDto
    {
        public string CustomerCode { get; set; } = null!;
        public decimal? CreditLimit { get; set; }
    }
}
