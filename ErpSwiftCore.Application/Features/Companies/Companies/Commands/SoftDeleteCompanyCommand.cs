using MediatR; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    public class SoftDeleteCompanyCommand : IRequest<APIResponseDto>
    {
        public Guid CompanyId { get; }

        public SoftDeleteCompanyCommand(Guid companyId)
        {
            CompanyId = companyId;
        }
    }

}
