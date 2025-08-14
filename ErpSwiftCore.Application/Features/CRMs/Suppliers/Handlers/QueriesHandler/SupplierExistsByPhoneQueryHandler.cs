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
    // 7. SupplierExistsByPhoneQueryHandler
    public class SupplierExistsByPhoneQueryHandler : BaseHandler<SupplierExistsByPhoneQuery>
    {
        private readonly ISupplierValidationService _validation;

        public SupplierExistsByPhoneQueryHandler(
            ISupplierValidationService validation,
            ILogger<BaseHandler<SupplierExistsByPhoneQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            SupplierExistsByPhoneQuery request,
            CancellationToken ct)
        {
            return await _validation.SupplierExistsByPhoneAsync(request.Phone, ct);
        }
    }
}
