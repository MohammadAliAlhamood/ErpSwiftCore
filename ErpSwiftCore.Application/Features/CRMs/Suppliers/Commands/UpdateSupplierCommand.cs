using ErpSwiftCore.Application.Features.CRMs.Suppliers.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands
{
    public class UpdateSupplierCommand : IRequest<APIResponseDto>
    {
        public UpdateSupplierDto Dto { get; }
        public UpdateSupplierCommand(UpdateSupplierDto dto) => Dto = dto;
    }

}
