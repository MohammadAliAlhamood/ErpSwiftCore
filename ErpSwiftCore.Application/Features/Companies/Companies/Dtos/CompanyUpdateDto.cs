using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using ErpSwiftCore.SharedKernel.Enums;
namespace ErpSwiftCore.Application.Features.Companies.Companies.Dtos
{
    public class CompanyUpdateDto :CompanyCreateDto
    {
        public Guid Id { get; set; }
 
    }

}
