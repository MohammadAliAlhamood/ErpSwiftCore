using ErpSwiftCore.Application.Features.CRMs.Customers.Queries;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using Microsoft.Extensions.Logging;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.QueriesHandler
{
    public class CustomerExistsByEmailExcludingQueryHandler : BaseHandler<CustomerExistsByEmailExcludingQuery>
    {
        private readonly ICustomerValidationService _validation;

        public CustomerExistsByEmailExcludingQueryHandler(
            ICustomerValidationService validation,
            ILogger<BaseHandler<CustomerExistsByEmailExcludingQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CustomerExistsByEmailExcludingQuery request,
            CancellationToken ct)
        {
            return await _validation.CustomerExistsByEmailAsync(
                request.Email,
                request.ExcludingId,
                ct);
        }
    }
}
