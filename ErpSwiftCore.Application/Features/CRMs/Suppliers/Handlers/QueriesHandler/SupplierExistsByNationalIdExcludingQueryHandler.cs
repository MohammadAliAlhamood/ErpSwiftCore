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
    // 6. SupplierExistsByNationalIdExcludingQueryHandler
    public class SupplierExistsByNationalIdExcludingQueryHandler : BaseHandler<SupplierExistsByNationalIdExcludingQuery>
    {
        private readonly ISupplierValidationService _validation;

        public SupplierExistsByNationalIdExcludingQueryHandler(
            ISupplierValidationService validation,
            ILogger<BaseHandler<SupplierExistsByNationalIdExcludingQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            SupplierExistsByNationalIdExcludingQuery request,
            CancellationToken ct)
        {
            return await _validation.SupplierExistsByNationalIdAsync(
                request.NationalId,
                request.ExcludingId,
                ct);
        }
    }
}
