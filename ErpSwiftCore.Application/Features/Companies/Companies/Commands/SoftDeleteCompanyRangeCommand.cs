using MediatR;
namespace ErpSwiftCore.Application.Features.Companies.Companies.Commands
{
    public class SoftDeleteCompanyRangeCommand : IRequest<APIResponseDto>
    {
        public IEnumerable<Guid> Ids { get; }

        public SoftDeleteCompanyRangeCommand(IEnumerable<Guid> ids)
        {
            Ids = ids;
        }
    }
}
