using ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Handlers.QueriesHandler
{
    public class GetSoftDeletedCategoriesCountQueryHandler : BaseHandler<GetSoftDeletedCategoriesCountQuery>
    {
        private readonly IProductCategoryQueryService _service;

        public GetSoftDeletedCategoriesCountQueryHandler(
            IProductCategoryQueryService service,
            ILogger<GetSoftDeletedCategoriesCountQueryHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedCategoriesCountQuery request, CancellationToken ct)
        {
            var count = await _service.GetSoftDeletedCategoriesCountAsync(ct);
            return new { Count = count };
        }
    }
}
