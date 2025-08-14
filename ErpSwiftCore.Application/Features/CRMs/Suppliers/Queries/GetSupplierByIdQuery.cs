using MediatR; 
namespace ErpSwiftCore.Application.Features.CRMs.Supplies.Queries
{
    public class GetSupplierByIdQuery : IRequest<APIResponseDto>
    {
        public Guid SupplierId { get; }
        public GetSupplierByIdQuery(Guid supplierId) => SupplierId = supplierId;
    } 
}