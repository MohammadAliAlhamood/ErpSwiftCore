using Microsoft.Extensions.Logging;
using ErpSwiftCore.Application.Features.CRMs.Customers.Commands;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.CommandsHandler
{
    public class DeleteCustomersRangeCommandHandler
        : BaseHandler<DeleteCustomersRangeCommand>
    {
        private readonly ICustomerCommandService _svc;
        public DeleteCustomersRangeCommandHandler(ICustomerCommandService svc, ILogger<BaseHandler<DeleteCustomersRangeCommand>> logger) : base(logger)
        {
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteCustomersRangeCommand req, CancellationToken ct)
        {
            var ok = await _svc.DeleteCustomersRangeAsync(req.Ids, ct);
            return new { Success = ok };
        }
    }

}
