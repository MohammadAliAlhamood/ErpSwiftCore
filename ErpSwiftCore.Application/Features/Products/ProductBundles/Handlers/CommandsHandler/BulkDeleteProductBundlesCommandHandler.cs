using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Handlers.CommandsHandler
{
    public class BulkDeleteProductBundlesCommandHandler : BaseHandler<BulkDeleteProductBundlesCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public BulkDeleteProductBundlesCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<BulkDeleteProductBundlesCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(BulkDeleteProductBundlesCommand request, CancellationToken cancellationToken)
        {
            var count = await _commandService.BulkDeleteBundlesAsync(request.BundleIds, cancellationToken);
            return new { DeletedCount = count };
        }
    }


}
