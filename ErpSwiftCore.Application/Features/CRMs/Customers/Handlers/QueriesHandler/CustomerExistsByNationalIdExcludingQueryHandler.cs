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
    public class CustomerExistsByNationalIdExcludingQueryHandler : BaseHandler<CustomerExistsByNationalIdExcludingQuery>
    {
        private readonly ICustomerValidationService _validation;

        public CustomerExistsByNationalIdExcludingQueryHandler(
            ICustomerValidationService validation,
            ILogger<BaseHandler<CustomerExistsByNationalIdExcludingQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CustomerExistsByNationalIdExcludingQuery request,
            CancellationToken ct)
        {
            return await _validation.CustomerExistsByNationalIdAsync(
                request.NationalId,
                request.ExcludingId,
                ct);
        }
    }
}
