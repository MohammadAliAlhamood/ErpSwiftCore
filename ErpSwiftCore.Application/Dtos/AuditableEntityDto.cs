using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
namespace ErpSwiftCore.Application.Dtos
{
    public class AuditableEntityDto : BaseEntityDto
    { 
        public CompanyDto? Company { get; set; }
    }
}