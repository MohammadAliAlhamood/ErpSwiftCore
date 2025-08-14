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
    public class CustomerExistsByPhoneQueryHandler : BaseHandler<CustomerExistsByPhoneQuery>
    {
        private readonly ICustomerValidationService _validation;

        public CustomerExistsByPhoneQueryHandler(
            ICustomerValidationService validation,
            ILogger<BaseHandler<CustomerExistsByPhoneQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CustomerExistsByPhoneQuery request,
            CancellationToken ct)
        {
            return await _validation.CustomerExistsByPhoneAsync(request.Phone, ct);
        }
    }
}
