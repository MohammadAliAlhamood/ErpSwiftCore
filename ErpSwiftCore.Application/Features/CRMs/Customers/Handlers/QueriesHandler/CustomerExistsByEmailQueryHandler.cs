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
    public class CustomerExistsByEmailQueryHandler : BaseHandler<CustomerExistsByEmailQuery>
    {
        private readonly ICustomerValidationService _validation;

        public CustomerExistsByEmailQueryHandler(
            ICustomerValidationService validation,
            ILogger<BaseHandler<CustomerExistsByEmailQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CustomerExistsByEmailQuery request,
            CancellationToken ct)
        {
            return await _validation.CustomerExistsByEmailAsync(request.Email, ct);
        }
    }

}
