using ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Handlers.CommandsHandler
{
    public class DeleteSupplierCommandHandler   : BaseHandler<DeleteSupplierCommand>
    {
        private readonly ISupplierCommandService _svc;
        public DeleteSupplierCommandHandler(ISupplierCommandService svc, ILogger<BaseHandler<DeleteSupplierCommand>> logger) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteSupplierCommand req, CancellationToken ct)
        {
            var ok = await _svc.DeleteSupplierAsync(req.Id, ct);
            return new { Success = ok };
        }
    }

}
