using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetPricesCountQueryHandler : BaseHandler<GetPricesCountQuery>
    {
        private readonly IProductPriceQueryService _svc;

        public GetPricesCountQueryHandler(
            IProductPriceQueryService svc,
            ILogger<BaseHandler<GetPricesCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(GetPricesCountQuery req, CancellationToken ct)
        {
            return await _svc.GetPricesCountAsync(ct);
        }
    }

}
