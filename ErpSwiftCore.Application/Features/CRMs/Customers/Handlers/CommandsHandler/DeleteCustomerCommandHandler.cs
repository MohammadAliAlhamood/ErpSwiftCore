using Microsoft.Extensions.Logging;
using ErpSwiftCore.Application.Features.CRMs.Customers.Commands;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.CommandsHandler
{
    public class DeleteCustomerCommandHandler : BaseHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerCommandService _svc;
        public DeleteCustomerCommandHandler(ICustomerCommandService svc, ILogger<BaseHandler<DeleteCustomerCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteCustomerCommand req, CancellationToken ct)
        {
            var ok = await _svc.DeleteCustomerAsync(req.CustomerID, ct);
            return new { Success = ok };
        }
    }

}
