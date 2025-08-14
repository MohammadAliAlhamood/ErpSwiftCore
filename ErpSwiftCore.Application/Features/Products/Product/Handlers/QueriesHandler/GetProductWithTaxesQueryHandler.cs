using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
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

    public class GetProductWithTaxesQueryHandler : BaseHandler<GetProductWithTaxesQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;

        public GetProductWithTaxesQueryHandler(
            IProductQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetProductWithTaxesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetProductWithTaxesQuery req, CancellationToken ct)
        {
            var e = await _svc.GetProductWithTaxesAsync(req.ProductId, ct);
            return _mapper.Map<ProductDto?>(e);
        }
    }

}
