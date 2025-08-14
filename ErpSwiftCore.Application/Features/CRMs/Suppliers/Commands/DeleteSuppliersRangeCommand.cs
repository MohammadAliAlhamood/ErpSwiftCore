using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands
{
    public class DeleteSuppliersRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> Ids { get; }
        public DeleteSuppliersRangeCommand(IEnumerable<Guid> ids) => Ids = ids;
    }
}
