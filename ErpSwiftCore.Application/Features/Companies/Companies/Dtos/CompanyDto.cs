using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using ErpSwiftCore.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Dtos
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = string.Empty; 
        public string TaxID { get; set; } = string.Empty;
        public IndustryType IndustryType { get; set; } = IndustryType.Unknown;
        public AddressDto? Address { get; set; }
        public ContactInfoDto ContactInfo { get; set; } = new();
        public bool IsActive { get; set; }
    }

}
