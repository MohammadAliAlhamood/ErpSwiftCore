using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Handlers.QueriesHandler
{
    public class GetProductUnitConversionWithProductQueryHandler
       : BaseHandler<GetProductUnitConversionWithProductQuery>
    {
        private readonly IProductUnitConversionQueryService _svc;
        private readonly IMapper _mapper;
        public GetProductUnitConversionWithProductQueryHandler(
            IProductUnitConversionQueryService svc,
            IMapper mapper,
            ILogger<GetProductUnitConversionWithProductQueryHandler> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetProductUnitConversionWithProductQuery request,
            CancellationToken ct)
        {
            var e = await _svc.GetUnitConversionWithProductAsync(request.ConversionId, ct);
            return _mapper.Map<ProductUnitConversionDto>(e);
        }
    }

}
