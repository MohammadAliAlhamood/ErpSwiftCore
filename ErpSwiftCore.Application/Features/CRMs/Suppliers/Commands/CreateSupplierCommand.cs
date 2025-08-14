using ErpSwiftCore.Application.Features.CRMs.Suppliers.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Supplies.Commands
{
    public class CreateSupplierCommand : IRequest<APIResponseDto>
    {
        public CreateSupplierDto Dto { get; }
        public CreateSupplierCommand(CreateSupplierDto dto) => Dto = dto;
    }


    

}