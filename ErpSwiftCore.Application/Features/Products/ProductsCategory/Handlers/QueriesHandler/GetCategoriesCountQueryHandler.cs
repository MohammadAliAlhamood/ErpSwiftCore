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


    public class GetCategoriesCountQueryHandler : BaseHandler<GetCategoriesCountQuery>
    {
        private readonly IProductCategoryQueryService _service;

        public GetCategoriesCountQueryHandler(
            IProductCategoryQueryService service,
            ILogger<GetCategoriesCountQueryHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(GetCategoriesCountQuery request, CancellationToken ct)
        {
            var count = await _service.GetCategoriesCountAsync(ct);
            return new { Count = count };
        }
    }


}
