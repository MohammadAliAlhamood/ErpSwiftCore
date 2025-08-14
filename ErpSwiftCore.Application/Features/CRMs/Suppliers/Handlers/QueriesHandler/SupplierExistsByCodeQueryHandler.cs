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
    // 2. SupplierExistsByCodeQueryHandler
    public class SupplierExistsByCodeQueryHandler : BaseHandler<SupplierExistsByCodeQuery>
    {
        private readonly ISupplierValidationService _validation;

        public SupplierExistsByCodeQueryHandler(
            ISupplierValidationService validation,
            ILogger<BaseHandler<SupplierExistsByCodeQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            SupplierExistsByCodeQuery request,
            CancellationToken ct)
        {
            return await _validation.SupplierExistsByCodeAsync(request.SupplierCode, ct);
        }
    }
    }