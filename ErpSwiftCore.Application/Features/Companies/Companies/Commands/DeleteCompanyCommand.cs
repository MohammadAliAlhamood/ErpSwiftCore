using MediatR; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    // Delete (soft‐delete) a single company by its ID
    public class DeleteCompanyCommand : IRequest<APIResponseDto>
    { 
        public  Guid  Id  { get; } 
        public DeleteCompanyCommand( Guid id)
        {
            Id = id;
        }
    }

}
