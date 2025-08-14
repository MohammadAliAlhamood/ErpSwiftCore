using ErpSwiftCore.Application.Features.CRMs.Customers.Commands;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.CommandsHandler
{
    public class RestoreCustomersRangeCommandHandler : BaseHandler<RestoreCustomersRangeCommand>
    {
        private readonly ICustomerCommandService _svc;
        public RestoreCustomersRangeCommandHandler(ICustomerCommandService svc, ILogger<BaseHandler<RestoreCustomersRangeCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreCustomersRangeCommand req, CancellationToken ct)
        {
            var ok = await _svc.RestoreCustomersRangeAsync(req.Ids, ct);
            return new { Success = ok };
        }
    }
}
