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
    public class BulkDeleteCategoriesCommandHandler : BaseHandler<BulkDeleteCategoriesCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public BulkDeleteCategoriesCommandHandler(
            IProductCategoryCommandService service,
            ILogger<BulkDeleteCategoriesCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(BulkDeleteCategoriesCommand request, CancellationToken ct)
        {
            var count = await _service.BulkDeleteCategoriesAsync(request.CategoryIds, ct);
            return new { DeletedCount = count };
        }
    }
}
