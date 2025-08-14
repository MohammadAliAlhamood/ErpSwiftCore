using AutoMapper;
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
    public class GetProductTaxWithProductQueryHandler : BaseHandler<GetProductTaxWithProductQuery>
    {
        private readonly IProductTaxQueryService _svc;
        private readonly IMapper _mapper;
        public GetProductTaxWithProductQueryHandler(IProductTaxQueryService svc, IMapper mapper, ILogger<GetProductTaxWithProductQueryHandler> logger)
            : base(logger) { _svc = svc; _mapper = mapper; }

        protected override async Task<object?> HandleRequestAsync(GetProductTaxWithProductQuery request, CancellationToken ct)
        {
            var e = await _svc.GetTaxWithProductAsync(request.TaxId, ct);
            return _mapper.Map<ProductTaxDto?>(e);
        }
    }
}
