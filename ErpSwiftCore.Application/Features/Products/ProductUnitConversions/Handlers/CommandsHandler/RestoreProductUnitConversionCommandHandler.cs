using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Handlers.CommandsHandler
{

    public class RestoreProductUnitConversionCommandHandler
        : BaseHandler<RestoreProductUnitConversionCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public RestoreProductUnitConversionCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<RestoreProductUnitConversionCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreProductUnitConversionCommand request,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreUnitConversionAsync(request.ConversionId, ct);
            return new { Restored = ok };
        }
    }


}
