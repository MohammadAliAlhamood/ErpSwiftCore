using MediatR; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    public class SoftDeleteAllCompaniesCommand : IRequest<APIResponseDto> { }
}
