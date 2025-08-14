using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Handlers.QueriesHandler
{
    public class GetSoftDeletedProductTaxesCountQueryHandler : BaseHandler<GetSoftDeletedProductTaxesCountQuery>
    {
        private readonly IProductTaxQueryService _svc;
        public GetSoftDeletedProductTaxesCountQueryHandler(IProductTaxQueryService svc, ILogger<GetSoftDeletedProductTaxesCountQueryHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedProductTaxesCountQuery request, CancellationToken ct)
        {
            var count = await _svc.GetSoftDeletedTaxesCountAsync(ct);
            return new { Count = count };
        }
    }


}
