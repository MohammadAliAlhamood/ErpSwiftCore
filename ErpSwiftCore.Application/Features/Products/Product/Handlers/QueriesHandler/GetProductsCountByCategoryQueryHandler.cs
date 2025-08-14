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

    public class GetProductsCountByCategoryQueryHandler : BaseHandler<GetProductsCountByCategoryQuery>
    {
        private readonly IProductQueryService _svc;

        public GetProductsCountByCategoryQueryHandler(
            IProductQueryService svc,
            ILogger<BaseHandler<GetProductsCountByCategoryQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(GetProductsCountByCategoryQuery req, CancellationToken ct)
        {
            var count = await _svc.GetProductsCountByCategoryAsync(req.CategoryId, ct);
            return new { Count = count };
        }
    }

}
