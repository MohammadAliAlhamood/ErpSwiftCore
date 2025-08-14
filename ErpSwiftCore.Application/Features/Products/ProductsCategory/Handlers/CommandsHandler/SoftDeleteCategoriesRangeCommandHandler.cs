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
    public class SoftDeleteCategoriesRangeCommandHandler : BaseHandler<SoftDeleteCategoriesRangeCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public SoftDeleteCategoriesRangeCommandHandler(
            IProductCategoryCommandService service,
            ILogger<SoftDeleteCategoriesRangeCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(SoftDeleteCategoriesRangeCommand request, CancellationToken ct)
        {
            var success = await _service.SoftDeleteCategoriesRangeAsync(request.CategoryIds, ct);
            return new { Success = success };
        }
    }

}
