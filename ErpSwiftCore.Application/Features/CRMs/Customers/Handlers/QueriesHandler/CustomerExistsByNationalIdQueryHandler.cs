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
    public class CustomerExistsByNationalIdQueryHandler : BaseHandler<CustomerExistsByNationalIdQuery>
    {
        private readonly ICustomerValidationService _validation;

        public CustomerExistsByNationalIdQueryHandler(
            ICustomerValidationService validation,
            ILogger<BaseHandler<CustomerExistsByNationalIdQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CustomerExistsByNationalIdQuery request,
            CancellationToken ct)
        {
            return await _validation.CustomerExistsByNationalIdAsync(request.NationalId, ct);
        }
    }
}
