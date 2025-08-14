using MediatR;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{
    public class CustomerExistsByPhoneExcludingQuery : IRequest<APIResponseDto>
    {
        public string Phone { get; }
        public Guid ExcludingId { get; }
        public CustomerExistsByPhoneExcludingQuery(string phone, Guid excludingId)
        {
            Phone = phone;
            ExcludingId = excludingId;
        }
    }
}