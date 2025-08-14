using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{
    public class GetSoftDeletedProductsCountQueryHandler : BaseHandler<GetSoftDeletedProductsCountQuery>
    {
        private readonly IProductQueryService _svc;

        public GetSoftDeletedProductsCountQueryHandler(
            IProductQueryService svc,
            ILogger<BaseHandler<GetSoftDeletedProductsCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedProductsCountQuery req, CancellationToken ct)
        {
            var count = await _svc.GetSoftDeletedProductsCountAsync(ct);
            return new { Count = count };
        }
    }

}
