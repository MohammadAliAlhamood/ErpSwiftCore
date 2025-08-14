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

    public class DeleteAllCategoriesCommandHandler : BaseHandler<DeleteAllCategoriesCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public DeleteAllCategoriesCommandHandler(
            IProductCategoryCommandService service,
            ILogger<DeleteAllCategoriesCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteAllCategoriesCommand request, CancellationToken ct)
        {
            var success = await _service.DeleteAllCategoriesAsync(ct);
            return new { Success = success };
        }
    }

}
