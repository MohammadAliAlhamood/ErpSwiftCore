using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class UpdateProductPriceCommandHandler : BaseHandler<UpdateProductPriceCommand>
    {
        private readonly IProductPriceCommandService _svc;
        private readonly IMapper _mapper;

        public UpdateProductPriceCommandHandler(
            IProductPriceCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdateProductPriceCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateProductPriceCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<ProductPrice>(req.Price);
            await _svc.UpdatePriceAsync(entity, ct);
            return new { UpdatedId = req.Price.Id };
        }
    }


}
