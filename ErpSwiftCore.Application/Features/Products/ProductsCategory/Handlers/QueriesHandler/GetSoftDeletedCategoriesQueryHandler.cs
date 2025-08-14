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

    public class GetSoftDeletedCategoriesQueryHandler : BaseHandler<GetSoftDeletedCategoriesQuery>
    {
        private readonly IProductCategoryQueryService _service;
        private readonly IMapper _mapper;

        public GetSoftDeletedCategoriesQueryHandler(
            IProductCategoryQueryService service,
            IMapper mapper,
            ILogger<GetSoftDeletedCategoriesQueryHandler> logger
        ) : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedCategoriesQuery request, CancellationToken ct)
        {
            var list = await _service.GetSoftDeletedCategoriesAsync(ct);
            return list.Select(e => _mapper.Map<ProductCategoryDto>(e)).ToList();
        }
    }

}
