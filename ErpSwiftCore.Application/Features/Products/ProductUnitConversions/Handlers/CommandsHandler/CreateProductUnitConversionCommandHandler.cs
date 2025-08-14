using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Handlers.CommandsHandler
{
    public class CreateProductUnitConversionCommandHandler : BaseHandler<CreateProductUnitConversionCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        private readonly IMapper _mapper;
        public CreateProductUnitConversionCommandHandler(
            IProductUnitConversionCommandService svc,
            IMapper mapper,
            ILogger<CreateProductUnitConversionCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            CreateProductUnitConversionCommand request,
            CancellationToken ct)
        {
            var entity = _mapper.Map<ProductUnitConversion>(request.Dto);
            var id = await _svc.CreateUnitConversionAsync(entity, ct);
            return new { CreatedId = id };
        }
    }
}

