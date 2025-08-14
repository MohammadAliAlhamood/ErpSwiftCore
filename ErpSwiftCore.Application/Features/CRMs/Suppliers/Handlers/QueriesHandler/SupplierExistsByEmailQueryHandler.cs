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
    // 3. SupplierExistsByEmailQueryHandler
    public class SupplierExistsByEmailQueryHandler : BaseHandler<SupplierExistsByEmailQuery>
    {
        private readonly ISupplierValidationService _validation;

        public SupplierExistsByEmailQueryHandler(
            ISupplierValidationService validation,
            ILogger<BaseHandler<SupplierExistsByEmailQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            SupplierExistsByEmailQuery request,
            CancellationToken ct)
        {
            return await _validation.SupplierExistsByEmailAsync(request.Email, ct);
        }
    }
    }
