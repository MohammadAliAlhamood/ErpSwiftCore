using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
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


    public class GetCategoryWithSubCategoriesQueryHandler : BaseHandler<GetCategoryWithSubCategoriesQuery>
    {
        private readonly IProductCategoryQueryService _service;
        private readonly IMapper _mapper;

        public GetCategoryWithSubCategoriesQueryHandler(
            IProductCategoryQueryService service,
            IMapper mapper,
            ILogger<GetCategoryWithSubCategoriesQueryHandler> logger
        ) : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCategoryWithSubCategoriesQuery request, CancellationToken ct)
        {
            var entity = await _service.GetCategoryWithSubCategoriesAsync(request.CategoryId, ct);
            return _mapper.Map<ProductCategoryDto>(entity);
        }
    }
}
