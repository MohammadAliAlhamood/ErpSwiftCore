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


    public class GetCategoriesByIdsQueryHandler : BaseHandler<GetCategoriesByIdsQuery>
    {
        private readonly IProductCategoryQueryService _service;
        private readonly IMapper _mapper;

        public GetCategoriesByIdsQueryHandler(
            IProductCategoryQueryService service,
            IMapper mapper,
            ILogger<GetCategoriesByIdsQueryHandler> logger
        ) : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCategoriesByIdsQuery request, CancellationToken ct)
        {
            var list = await _service.GetCategoriesByIdsAsync(request.CategoryIds, ct);
            return list.Select(e => _mapper.Map<ProductCategoryDto>(e)).ToList();
        }
    }
}
