using MediatR; 
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands
{
    public class DeleteSupplierCommand : IRequest<APIResponseDto>
    {
        public Guid  Id { get; }
        public DeleteSupplierCommand(Guid id) => Id = id;
    }

}
