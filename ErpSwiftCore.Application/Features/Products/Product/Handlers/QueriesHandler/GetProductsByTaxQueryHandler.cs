using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 

namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{

    public class GetProductsByTaxQueryHandler : BaseHandler<GetProductsByTaxQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;

        public GetProductsByTaxQueryHandler(
            IProductQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetProductsByTaxQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetProductsByTaxQuery req, CancellationToken ct)
        {
            var list = await _svc.GetProductsByTaxAsync(req.TaxId, ct);
            return list.Select(e => _mapper.Map<ProductDto>(e));
        }
    }


}
