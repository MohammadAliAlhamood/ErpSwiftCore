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

    public class GetSoftDeletedCategoryByIdQueryHandler : BaseHandler<GetSoftDeletedCategoryByIdQuery>
    {
        private readonly IProductCategoryQueryService _service;
        private readonly IMapper _mapper;

        public GetSoftDeletedCategoryByIdQueryHandler(
            IProductCategoryQueryService service,
            IMapper mapper,
            ILogger<GetSoftDeletedCategoryByIdQueryHandler> logger
        ) : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedCategoryByIdQuery request, CancellationToken ct)
        {
            var entity = await _service.GetSoftDeletedCategoryByIdAsync(request.CategoryId, ct);
            return _mapper.Map<ProductCategoryDto>(entity);
        }
    }
}
