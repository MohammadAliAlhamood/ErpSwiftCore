using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.CommandsHandler
{

    public class BulkImportProductsCommandHandler : BaseHandler<BulkImportProductsCommand>
    {
        private readonly IProductCommandService _svc;
        private readonly IMapper _mapper;

        public BulkImportProductsCommandHandler(
            IProductCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<BulkImportProductsCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(BulkImportProductsCommand req, CancellationToken ct)
        {
            var entities = req.Dto.Products
                .Select(d => _mapper.Map<Domain.Entities.EntityProduct.Product>(d));
            var count = await _svc.BulkImportProductsAsync(entities, ct);
            return new { ImportedCount = count };
        }
    }


}
