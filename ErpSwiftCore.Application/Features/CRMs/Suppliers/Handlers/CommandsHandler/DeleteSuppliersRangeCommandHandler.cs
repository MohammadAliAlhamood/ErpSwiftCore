using ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Handlers.CommandsHandler
{
    public class DeleteSuppliersRangeCommandHandler : BaseHandler<DeleteSuppliersRangeCommand>
    {
        private readonly ISupplierCommandService _svc;
        public DeleteSuppliersRangeCommandHandler(ISupplierCommandService svc, ILogger<BaseHandler<DeleteSuppliersRangeCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteSuppliersRangeCommand req, CancellationToken ct)
        {
            var ok = await _svc.DeleteSuppliersRangeAsync(req.Ids, ct);
            return new { Success = ok };
        }
    }

}
