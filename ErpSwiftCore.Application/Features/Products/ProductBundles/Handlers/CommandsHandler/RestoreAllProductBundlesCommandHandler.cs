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
    public class RestoreAllProductBundlesCommandHandler : BaseHandler<RestoreAllProductBundlesCommand>
    {
        private readonly IProductBundleCommandService _commandService;
        public RestoreAllProductBundlesCommandHandler(
            IProductBundleCommandService commandService,
            ILogger<RestoreAllProductBundlesCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreAllProductBundlesCommand request, CancellationToken cancellationToken)
        {
            var success = await _commandService.RestoreAllBundlesAsync(cancellationToken);
            return new { RestoredAll = success };
        }
    }


}
