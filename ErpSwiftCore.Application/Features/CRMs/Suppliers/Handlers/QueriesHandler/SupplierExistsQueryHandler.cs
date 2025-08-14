using ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Handlers.QueriesHandler
{
    // 1. SupplierExistsQueryHandler
    public class SupplierExistsQueryHandler : BaseHandler<SupplierExistsQuery>
    {
        private readonly ISupplierValidationService _validation;

        public SupplierExistsQueryHandler(
            ISupplierValidationService validation,
            ILogger<BaseHandler<SupplierExistsQuery>> logger)
            : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            SupplierExistsQuery request,
            CancellationToken ct)
        {
            return await _validation.SupplierExistsAsync(request.SupplierId, ct);
        }
    }
}