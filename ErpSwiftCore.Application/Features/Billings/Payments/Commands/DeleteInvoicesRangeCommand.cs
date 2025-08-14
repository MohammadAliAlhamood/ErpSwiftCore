using MediatR; 
using ErpSwiftCore.Application.Features.Billings.Payments.Dtos;
namespace ErpSwiftCore.Application.Features.Billings.Payments.Commands
{
    public class DeleteInvoicesRangeCommand : IRequest<APIResponseDto>
    {
        public BatchDeleteInvoicesDto Dto { get; }
        public DeleteInvoicesRangeCommand(BatchDeleteInvoicesDto dto) => Dto = dto;
    }

}
