using ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Handlers.CommandsHandler
{
    public class RestoreAllCategoriesCommandHandler : BaseHandler<RestoreAllCategoriesCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public RestoreAllCategoriesCommandHandler(
            IProductCategoryCommandService service,
            ILogger<RestoreAllCategoriesCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreAllCategoriesCommand request, CancellationToken ct)
        {
            var success = await _service.RestoreAllCategoriesAsync(ct);
            return new { Success = success };
        }
    }

}
