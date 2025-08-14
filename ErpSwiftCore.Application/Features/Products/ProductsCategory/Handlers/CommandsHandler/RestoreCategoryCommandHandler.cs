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
    public class RestoreCategoryCommandHandler : BaseHandler<RestoreCategoryCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public RestoreCategoryCommandHandler(
            IProductCategoryCommandService service,
            ILogger<RestoreCategoryCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreCategoryCommand request, CancellationToken ct)
        {
            var success = await _service.RestoreCategoryAsync(request.CategoryId, ct);
            return new { Success = success };
        }
    }


}
