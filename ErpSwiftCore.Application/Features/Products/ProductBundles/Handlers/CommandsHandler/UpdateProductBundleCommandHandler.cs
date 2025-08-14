using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class UpdateProductBundleCommandHandler : BaseHandler<UpdateProductBundleCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        private readonly IMapper _mapper;
        public UpdateProductBundleCommandHandler(
            IProductBundleCommandService commandService,
            IMapper mapper,
            ILogger<UpdateProductBundleCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(UpdateProductBundleCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ProductBundle>(request.Bundle);
            var success = await _commandService.UpdateBundleAsync(entity, cancellationToken);
            return new { Updated = success };
        }
    }


}
