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
    // 5. SupplierExistsByNationalIdQueryHandler
    public class SupplierExistsByNationalIdQueryHandler : BaseHandler<SupplierExistsByNationalIdQuery>
    {
        private readonly ISupplierValidationService _validation;

        public SupplierExistsByNationalIdQueryHandler(
            ISupplierValidationService validation,
            ILogger<BaseHandler<SupplierExistsByNationalIdQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            SupplierExistsByNationalIdQuery request,
            CancellationToken ct)
        {
            return await _validation.SupplierExistsByNationalIdAsync(request.NationalId, ct);
        }
    }
}
