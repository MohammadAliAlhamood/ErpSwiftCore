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




    public class GetCategoriesCountByParentQueryHandler : BaseHandler<GetCategoriesCountByParentQuery>
    {
        private readonly IProductCategoryQueryService _service;

        public GetCategoriesCountByParentQueryHandler(
            IProductCategoryQueryService service,
            ILogger<GetCategoriesCountByParentQueryHandler> logger
        ) : base(logger)
        {
            _service = service;
        }

        protected override async Task<object?> HandleRequestAsync(GetCategoriesCountByParentQuery request, CancellationToken ct)
        {
            var count = await _service.GetCategoriesCountByParentAsync(request.ParentCategoryId, request.Recursive, ct);
            return new { Count = count };
        }
    }


}
