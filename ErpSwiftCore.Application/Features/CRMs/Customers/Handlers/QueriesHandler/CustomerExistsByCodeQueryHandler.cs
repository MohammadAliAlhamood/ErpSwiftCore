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
    // 2. CustomerExistsByCodeQueryHandler
    public class CustomerExistsByCodeQueryHandler : BaseHandler<CustomerExistsByCodeQuery>
    {
        private readonly ICustomerValidationService _validation;

        public CustomerExistsByCodeQueryHandler(
            ICustomerValidationService validation,
            ILogger<BaseHandler<CustomerExistsByCodeQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CustomerExistsByCodeQuery request,
            CancellationToken ct)
        {
            return await _validation.CustomerExistsByCodeAsync(request.CustomerCode, ct);
        }
    }
}
