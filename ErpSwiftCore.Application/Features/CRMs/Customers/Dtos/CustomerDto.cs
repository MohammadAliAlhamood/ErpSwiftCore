using ErpSwiftCore.Application.Dtos.Person;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Dtos
{
    /// <summary>
    /// بيانات تمثيل الـCustomer مع كافة الخصائص والمعلومات القابلة للعرض
    /// </summary>
    public class CustomerDto : PersonDto
    {
        public string CustomerCode { get; set; } = null!;
        public decimal? CreditLimit { get; set; }
    }

}
