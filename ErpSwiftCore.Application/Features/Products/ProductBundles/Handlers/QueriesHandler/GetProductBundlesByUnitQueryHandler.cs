using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.QueriesHandler
{
    public class GetProductBundlesByUnitQueryHandler : BaseHandler<GetProductBundlesByUnitQuery>
    {
        private readonly IProductBundleQueryService _queryService;
        private readonly IMapper _mapper;
        public GetProductBundlesByUnitQueryHandler(
            IProductBundleQueryService queryService,
            IMapper mapper,
            ILogger<GetProductBundlesByUnitQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(GetProductBundlesByUnitQuery request, CancellationToken cancellationToken)
        {
            var entities = await _queryService.GetBundlesByUnitAsync(request.UnitOfMeasurementId, cancellationToken);
            var dtos = entities.Select(e => _mapper.Map<ProductBundleDto>(e)).ToList();
            return dtos;
        }
    }


}
