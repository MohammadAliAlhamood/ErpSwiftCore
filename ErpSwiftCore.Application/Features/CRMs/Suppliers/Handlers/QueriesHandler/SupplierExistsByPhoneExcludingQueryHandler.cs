using ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Handlers.QueriesHandler
{
    // 8. SupplierExistsByPhoneExcludingQueryHandler
    public class SupplierExistsByPhoneExcludingQueryHandler : BaseHandler<SupplierExistsByPhoneExcludingQuery>
    {
        private readonly ISupplierValidationService _validation;

        public SupplierExistsByPhoneExcludingQueryHandler(
            ISupplierValidationService validation,
            ILogger<BaseHandler<SupplierExistsByPhoneExcludingQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            SupplierExistsByPhoneExcludingQuery request,
            CancellationToken ct)
        {
            return await _validation.SupplierExistsByPhoneAsync(
                request.Phone,
                request.ExcludingId,
                ct);
        }
    }
}
