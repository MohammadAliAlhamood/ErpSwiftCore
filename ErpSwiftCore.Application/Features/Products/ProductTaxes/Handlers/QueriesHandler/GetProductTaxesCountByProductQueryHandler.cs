using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Handlers.QueriesHandler
{
    public class GetProductTaxesCountByProductQueryHandler : BaseHandler<GetProductTaxesCountByProductQuery>
    {
        private readonly IProductTaxQueryService _svc;
        public GetProductTaxesCountByProductQueryHandler(IProductTaxQueryService svc, ILogger<GetProductTaxesCountByProductQueryHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(GetProductTaxesCountByProductQuery request, CancellationToken ct)
        {
            var count = await _svc.GetTaxesCountByProductAsync(request.ProductId, ct);
            return new { Count = count };
        }
    }
}
