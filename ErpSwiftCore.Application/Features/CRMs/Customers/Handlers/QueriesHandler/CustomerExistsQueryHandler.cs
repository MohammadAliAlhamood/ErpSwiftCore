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
    // 1. CustomerExistsQueryHandler
    public class CustomerExistsQueryHandler : BaseHandler<CustomerExistsQuery>
    {
        private readonly ICustomerValidationService _validation;

        public CustomerExistsQueryHandler(
            ICustomerValidationService validation,
            ILogger<BaseHandler<CustomerExistsQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CustomerExistsQuery request,
            CancellationToken ct)
        {
            return await _validation.CustomerExistsAsync(request.CustomerId, ct);
        }
    }
}
