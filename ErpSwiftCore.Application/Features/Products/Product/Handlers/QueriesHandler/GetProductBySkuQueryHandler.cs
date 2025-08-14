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

    public class GetProductBySkuQueryHandler : BaseHandler<GetProductBySkuQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;

        public GetProductBySkuQueryHandler(
            IProductQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetProductBySkuQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetProductBySkuQuery req, CancellationToken ct)
        {
            var e = await _svc.GetProductBySkuAsync(req.SKU, ct);
            return _mapper.Map<ProductDto?>(e);
        }
    }



}
