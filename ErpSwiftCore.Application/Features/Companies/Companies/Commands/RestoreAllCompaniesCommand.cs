using MediatR; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    // Restore all SoftDeleted companies
    public class RestoreAllCompaniesCommand : IRequest<APIResponseDto> { }

}
