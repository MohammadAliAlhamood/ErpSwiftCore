using ErpSwiftCore.Application.Features.CRMs.Customers.Queries;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.QueriesHandler
{
    public class CustomerExistsByPhoneExcludingQueryHandler : BaseHandler<CustomerExistsByPhoneExcludingQuery>
    {
        private readonly ICustomerValidationService _validation;

        public CustomerExistsByPhoneExcludingQueryHandler(
            ICustomerValidationService validation,
            ILogger<BaseHandler<CustomerExistsByPhoneExcludingQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CustomerExistsByPhoneExcludingQuery request,
            CancellationToken ct)
        {
            return await _validation.CustomerExistsByPhoneAsync(
                request.Phone,
                request.ExcludingId,
                ct);
        }
    }
}