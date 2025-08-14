using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{
    public class GetProductsCountByTypeQueryHandler : BaseHandler<GetProductsCountByTypeQuery>
    {
        private readonly IProductQueryService _svc;

        public GetProductsCountByTypeQueryHandler(
            IProductQueryService svc,
            ILogger<BaseHandler<GetProductsCountByTypeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(GetProductsCountByTypeQuery req, CancellationToken ct)
        {
            var count = await _svc.GetProductsCountByTypeAsync(req.ProductTypeId, ct);
            return new { Count = count };
        }
    }

}
