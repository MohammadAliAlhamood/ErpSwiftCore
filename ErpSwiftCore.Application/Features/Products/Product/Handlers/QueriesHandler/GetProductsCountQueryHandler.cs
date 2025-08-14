using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{

    public class GetProductsCountQueryHandler : BaseHandler<GetProductsCountQuery>
    {
        private readonly IProductQueryService _svc;

        public GetProductsCountQueryHandler(
            IProductQueryService svc,
            ILogger<BaseHandler<GetProductsCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(GetProductsCountQuery req, CancellationToken ct)
        {
            var count = await _svc.GetProductsCountAsync(ct);
            return new { Count = count };
        }
    }


}
