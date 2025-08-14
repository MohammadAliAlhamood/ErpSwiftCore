using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class CreateProductPriceCommandHandler : BaseHandler<CreateProductPriceCommand>
    {
        private readonly IProductPriceCommandService _svc;
        private readonly IMapper _mapper; 
        public CreateProductPriceCommandHandler(   IProductPriceCommandService svc,    IMapper mapper,    ILogger<BaseHandler<CreateProductPriceCommand>> logger ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(CreateProductPriceCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<ProductPrice>(req.Price);
            var id = await _svc.CreatePriceAsync(entity, ct);
            return new { Id = id };
        }
    } 
}