using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands
{
    public class RestoreSuppliersRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> Ids { get; }
        public RestoreSuppliersRangeCommand(IEnumerable<Guid> ids) => Ids = ids;
    }

}
