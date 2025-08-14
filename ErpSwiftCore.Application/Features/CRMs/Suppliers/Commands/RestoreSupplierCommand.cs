using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands
{

    public class RestoreSupplierCommand : IRequest<APIResponseDto>
    {
        public Guid Id { get; }
        public RestoreSupplierCommand(Guid id) => Id = id;
    }

}
