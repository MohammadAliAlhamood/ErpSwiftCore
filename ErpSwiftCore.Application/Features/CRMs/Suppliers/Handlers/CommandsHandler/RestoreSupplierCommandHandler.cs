using ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Handlers.CommandsHandler
{
    public class RestoreSupplierCommandHandler : BaseHandler<RestoreSupplierCommand>
    {
        private readonly ISupplierCommandService _svc;

        public RestoreSupplierCommandHandler(ISupplierCommandService svc, ILogger<BaseHandler<RestoreSupplierCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreSupplierCommand req, CancellationToken ct)
        {
            var ok = await _svc.RestoreSupplierAsync(req.Id, ct);
            return new { Success = ok };
        }
    }


}
