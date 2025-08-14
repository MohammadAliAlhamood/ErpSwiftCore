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


    public class GetAllCategoriesQueryHandler : BaseHandler<GetAllCategoriesQuery>
    {
        private readonly IProductCategoryQueryService _service;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(
            IProductCategoryQueryService service,
            IMapper mapper,
            ILogger<GetAllCategoriesQueryHandler> logger
        ) : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetAllCategoriesQuery request, CancellationToken ct)
        {
            var list = await _service.GetAllCategoriesAsync(ct);
            return list.Select(e => _mapper.Map<ProductCategoryDto>(e)).ToList();
        }
    }
}
