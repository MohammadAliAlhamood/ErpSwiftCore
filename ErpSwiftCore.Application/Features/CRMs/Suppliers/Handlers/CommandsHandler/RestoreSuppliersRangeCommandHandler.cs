using ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Handlers.CommandsHandler
{
    public class RestoreSuppliersRangeCommandHandler : BaseHandler<RestoreSuppliersRangeCommand>
    {
        private readonly ISupplierCommandService _svc;

        public RestoreSuppliersRangeCommandHandler(ISupplierCommandService svc, ILogger<BaseHandler<RestoreSuppliersRangeCommand>> logger) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreSuppliersRangeCommand req, CancellationToken ct)
        {
            var ok = await _svc.RestoreSuppliersRangeAsync(req.Ids, ct);
            return new { Success = ok };
        }
    }


}
