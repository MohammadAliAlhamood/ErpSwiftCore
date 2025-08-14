using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    // Add multiple companies at once
    public class BulkCreateCompaniesCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<CompanyCreateDto> Companies { get; }

        public BulkCreateCompaniesCommand(IEnumerable<CompanyCreateDto> companies)
        {
            Companies = companies;
        }
    } 
}
