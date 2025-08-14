using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Handlers.QueriesHandler
{
    public class GetProductTaxByIdQueryHandler : BaseHandler<GetProductTaxByIdQuery>
    {
        private readonly IProductTaxQueryService _svc;
        private readonly IMapper _mapper;
        public GetProductTaxByIdQueryHandler(IProductTaxQueryService svc, IMapper mapper, ILogger<GetProductTaxByIdQueryHandler> logger)
            : base(logger) { _svc = svc; _mapper = mapper; }

        protected override async Task<object?> HandleRequestAsync(GetProductTaxByIdQuery request, CancellationToken ct)
        {
            var e = await _svc.GetTaxByIdAsync(request.TaxId, ct);
            return _mapper.Map<ProductTaxDto?>(e);
        }
    } 
}