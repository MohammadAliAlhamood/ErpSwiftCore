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
    public class RestoreCategoriesRangeCommandHandler : BaseHandler<RestoreCategoriesRangeCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public RestoreCategoriesRangeCommandHandler(
            IProductCategoryCommandService service,
            ILogger<RestoreCategoriesRangeCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreCategoriesRangeCommand request, CancellationToken ct)
        {
            var success = await _service.RestoreCategoriesRangeAsync(request.CategoryIds, ct);
            return new { Success = success };
        }
    }


}
