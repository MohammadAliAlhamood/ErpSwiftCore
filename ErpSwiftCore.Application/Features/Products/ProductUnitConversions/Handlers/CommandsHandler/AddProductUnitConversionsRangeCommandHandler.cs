using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Handlers.CommandsHandler
{
    public class AddProductUnitConversionsRangeCommandHandler
       : BaseHandler<AddProductUnitConversionsRangeCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        private readonly IMapper _mapper;
        public AddProductUnitConversionsRangeCommandHandler(
            IProductUnitConversionCommandService svc,
            IMapper mapper,
            ILogger<AddProductUnitConversionsRangeCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            AddProductUnitConversionsRangeCommand request,
            CancellationToken ct)
        {
            var entities = request.Dtos
                .Select(d => _mapper.Map<ProductUnitConversion>(d));
            var ids = await _svc.AddUnitConversionsRangeAsync(entities, ct);
            return new { CreatedIds = ids.ToList() };
        }
    }


}
