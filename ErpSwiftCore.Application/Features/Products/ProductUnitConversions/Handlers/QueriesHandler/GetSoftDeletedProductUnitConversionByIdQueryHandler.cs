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
    public class GetSoftDeletedProductUnitConversionByIdQueryHandler
       : BaseHandler<GetSoftDeletedProductUnitConversionByIdQuery>
    {
        private readonly IProductUnitConversionQueryService _svc;
        private readonly IMapper _mapper;
        public GetSoftDeletedProductUnitConversionByIdQueryHandler(
            IProductUnitConversionQueryService svc,
            IMapper mapper,
            ILogger<GetSoftDeletedProductUnitConversionByIdQueryHandler> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetSoftDeletedProductUnitConversionByIdQuery request,
            CancellationToken ct)
        {
            var e = await _svc.GetSoftDeletedUnitConversionByIdAsync(request.ConversionId, ct);
            return _mapper.Map<ProductUnitConversionDto>(e);
        }
    }

}
