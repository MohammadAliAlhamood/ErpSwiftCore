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

    public class DeleteCategoryCommandHandler : BaseHandler<DeleteCategoryCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public DeleteCategoryCommandHandler(
            IProductCategoryCommandService service,
            ILogger<DeleteCategoryCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteCategoryCommand request, CancellationToken ct)
        {
            var success = await _service.DeleteCategoryAsync(request.CategoryId, ct);
            return new { Success = success };
        }
    }


}
