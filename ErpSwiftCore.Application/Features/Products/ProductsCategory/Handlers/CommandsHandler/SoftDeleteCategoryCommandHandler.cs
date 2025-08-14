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
    public class SoftDeleteCategoryCommandHandler : BaseHandler<SoftDeleteCategoryCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public SoftDeleteCategoryCommandHandler(
            IProductCategoryCommandService service,
            ILogger<SoftDeleteCategoryCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(SoftDeleteCategoryCommand request, CancellationToken ct)
        {
            var success = await _service.SoftDeleteCategoryAsync(request.CategoryId, ct);
            return new { Success = success };
        }
    }

}
