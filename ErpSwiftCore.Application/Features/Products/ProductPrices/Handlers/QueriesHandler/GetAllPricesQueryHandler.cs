using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.QueriesHandler
{
    public class GetAllPricesQueryHandler : BaseHandler<GetAllPricesQuery>
    {
        private readonly IProductPriceQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllPricesQueryHandler(
            IProductPriceQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllPricesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetAllPricesQuery req, CancellationToken ct)
        {
            var list = await _svc.GetAllPricesAsync(ct);
            return _mapper.Map<IEnumerable<ProductPriceDto>>(list);
        }
    }


}
