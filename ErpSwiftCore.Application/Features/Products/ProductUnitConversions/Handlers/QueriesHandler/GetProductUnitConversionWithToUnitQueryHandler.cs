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

    public class GetProductUnitConversionWithToUnitQueryHandler
        : BaseHandler<GetProductUnitConversionWithToUnitQuery>
    {
        private readonly IProductUnitConversionQueryService _svc;
        private readonly IMapper _mapper;
        public GetProductUnitConversionWithToUnitQueryHandler(
            IProductUnitConversionQueryService svc,
            IMapper mapper,
            ILogger<GetProductUnitConversionWithToUnitQueryHandler> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetProductUnitConversionWithToUnitQuery request,
            CancellationToken ct)
        {
            var e = await _svc.GetUnitConversionWithToUnitAsync(request.ConversionId, ct);
            return _mapper.Map<ProductUnitConversionDto>(e);
        }
    }

}
