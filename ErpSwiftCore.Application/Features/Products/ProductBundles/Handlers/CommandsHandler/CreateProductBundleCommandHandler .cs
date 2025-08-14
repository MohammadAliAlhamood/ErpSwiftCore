using AutoMapper;
using Microsoft.Extensions.Logging;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class CreateProductBundleCommandHandler : BaseHandler<CreateProductBundleCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProductBundleCommandService _commandService;
        public CreateProductBundleCommandHandler(IProductBundleCommandService commandService, IMapper mapper, ILogger<CreateProductBundleCommandHandler> logger) : base(logger)
        {
            _commandService = commandService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(CreateProductBundleCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ProductBundle>(request.Bundle);
            var newId = await _commandService.CreateBundleAsync(entity, cancellationToken);
            return new { CreatedBundleId = newId };
        }
    }
}
