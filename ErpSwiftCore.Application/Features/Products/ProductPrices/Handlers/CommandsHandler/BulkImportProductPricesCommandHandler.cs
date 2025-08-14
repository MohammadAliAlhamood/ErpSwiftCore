using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Handlers.CommandsHandler
{
    public class BulkImportProductPricesCommandHandler : BaseHandler<BulkImportProductPricesCommand>
    {
        private readonly IProductPriceCommandService _svc;
        private readonly IMapper _mapper;
        public BulkImportProductPricesCommandHandler(IProductPriceCommandService svc, IMapper mapper, ILogger<BaseHandler<BulkImportProductPricesCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(BulkImportProductPricesCommand req, CancellationToken ct)
        {
            var entities = req.Prices.Select(dto => _mapper.Map<ProductPrice>(dto));
            var importedCount = await _svc.BulkImportPricesAsync(entities, ct);
            return new { Imported = importedCount };
        }
    }
}
