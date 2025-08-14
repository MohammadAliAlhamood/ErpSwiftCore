using MediatR; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Queries
{
    public class GetCompanyByIdQuery : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }
        public GetCompanyByIdQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
