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
    // 4. SupplierExistsByEmailExcludingQueryHandler
    public class SupplierExistsByEmailExcludingQueryHandler : BaseHandler<SupplierExistsByEmailExcludingQuery>
    {
        private readonly ISupplierValidationService _validation;

        public SupplierExistsByEmailExcludingQueryHandler(
            ISupplierValidationService validation,
            ILogger<BaseHandler<SupplierExistsByEmailExcludingQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            SupplierExistsByEmailExcludingQuery request,
            CancellationToken ct)
        {
            return await _validation.SupplierExistsByEmailAsync(
                request.Email,
                request.ExcludingId,
                ct);
        }
    }
    }