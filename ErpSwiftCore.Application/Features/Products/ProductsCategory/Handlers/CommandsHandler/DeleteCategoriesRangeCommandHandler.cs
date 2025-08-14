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
    public class DeleteCategoriesRangeCommandHandler : BaseHandler<DeleteCategoriesRangeCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public DeleteCategoriesRangeCommandHandler(
            IProductCategoryCommandService service,
            ILogger<DeleteCategoriesRangeCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteCategoriesRangeCommand request, CancellationToken ct)
        {
            var success = await _service.DeleteCategoriesRangeAsync(request.CategoryIds, ct);
            return new { Success = success };
        }
    }

}
