using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Handlers.CommandsHandler
{
    public class AddCategoriesRangeCommandHandler : BaseHandler<AddCategoriesRangeCommand>
    {
        private readonly IProductCategoryCommandService _service;
        private readonly IMapper _mapper;

        public AddCategoriesRangeCommandHandler(
            IProductCategoryCommandService service,
            IMapper mapper,
            ILogger<AddCategoriesRangeCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(AddCategoriesRangeCommand request, CancellationToken ct)
        {
            var entities = request.Categories.Select(dto => _mapper.Map<ProductCategory>(dto));
            var ids = await _service.AddCategoriesRangeAsync(entities, ct);
            return new { CategoryIds = ids.ToList() };
        }
    }


}
