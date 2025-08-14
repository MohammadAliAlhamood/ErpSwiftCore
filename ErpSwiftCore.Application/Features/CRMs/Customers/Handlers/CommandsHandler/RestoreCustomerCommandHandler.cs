using Microsoft.Extensions.Logging;
using ErpSwiftCore.Application.Features.CRMs.Customers.Commands;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.CommandsHandler
{
    public class RestoreCustomerCommandHandler : BaseHandler<RestoreCustomerCommand>
    {
        private readonly ICustomerCommandService _svc;

        public RestoreCustomerCommandHandler(ICustomerCommandService svc, ILogger<BaseHandler<RestoreCustomerCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreCustomerCommand req, CancellationToken ct)
        {
            var ok = await _svc.RestoreCustomerAsync(req.Id, ct);
            return new { Success = ok };
        }
    }

}
