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

    public class SoftDeleteAllCategoriesCommandHandler : BaseHandler<SoftDeleteAllCategoriesCommand>
    {
        private readonly IProductCategoryCommandService _service;

        public SoftDeleteAllCategoriesCommandHandler(
            IProductCategoryCommandService service,
            ILogger<SoftDeleteAllCategoriesCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(SoftDeleteAllCategoriesCommand request, CancellationToken ct)
        {
            var success = await _service.SoftDeleteAllCategoriesAsync(ct);
            return new { Success = success };
        }
    }


}
