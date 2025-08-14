using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetSoftDeletedPricesCountQueryHandler : BaseHandler<GetSoftDeletedPricesCountQuery>
    {
        private readonly IProductPriceQueryService _svc;

        public GetSoftDeletedPricesCountQueryHandler(
            IProductPriceQueryService svc,
            ILogger<BaseHandler<GetSoftDeletedPricesCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedPricesCountQuery req, CancellationToken ct)
        {
            return await _svc.GetSoftDeteltedPricesCountAsync(ct);
        }
    }


}
