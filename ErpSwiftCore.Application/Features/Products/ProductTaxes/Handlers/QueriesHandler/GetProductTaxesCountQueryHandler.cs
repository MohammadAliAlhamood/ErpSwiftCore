using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
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
    public class GetProductTaxesCountQueryHandler : BaseHandler<GetProductTaxesCountQuery>
    {
        private readonly IProductTaxQueryService _svc;
        public GetProductTaxesCountQueryHandler(IProductTaxQueryService svc, ILogger<GetProductTaxesCountQueryHandler> logger)
            : base(logger) { _svc = svc; }

        protected override async Task<object?> HandleRequestAsync(GetProductTaxesCountQuery request, CancellationToken ct)
        {
            var count = await _svc.GetTaxesCountAsync(ct);
            return new TaxCountByStatusDto { Active = count, Inactive = 0 };
        }
    }

}
