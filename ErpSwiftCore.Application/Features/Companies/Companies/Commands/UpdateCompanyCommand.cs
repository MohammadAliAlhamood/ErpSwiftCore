using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    // Update an existing company
    public class UpdateCompanyCommand : IRequest<APIResponseDto>
    {
        public CompanyUpdateDto Company { get; }

        public UpdateCompanyCommand(CompanyUpdateDto company)
        {
            Company = company;
        }
    }

}
