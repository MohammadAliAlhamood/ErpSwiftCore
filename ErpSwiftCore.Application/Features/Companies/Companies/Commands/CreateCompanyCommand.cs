using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{ 
    public class CreateCompanyCommand : IRequest<APIResponseDto>
    {
        public CompanyCreateDto Company { get; }
        public CreateCompanyCommand(CompanyCreateDto company)
        {
            Company = company;
        }
    }
}
