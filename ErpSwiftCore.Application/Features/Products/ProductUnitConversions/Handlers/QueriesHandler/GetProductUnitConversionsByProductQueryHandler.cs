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
    public class GetProductUnitConversionsByProductQueryHandler
       : BaseHandler<GetProductUnitConversionsByProductQuery>
    {
        private readonly IProductUnitConversionQueryService _svc;
        private readonly IMapper _mapper;
        public GetProductUnitConversionsByProductQueryHandler(
            IProductUnitConversionQueryService svc,
            IMapper mapper,
            ILogger<GetProductUnitConversionsByProductQueryHandler> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetProductUnitConversionsByProductQuery request,
            CancellationToken ct)
        {
            var list = await _svc.GetUnitConversionsByProductAsync(request.ProductId, ct);
            return list.Select(e => _mapper.Map<ProductUnitConversionDto>(e)).ToList();
        }
    }

}
