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
    public class CreateCategoryCommandHandler : BaseHandler<CreateCategoryCommand>
    {
        private readonly IProductCategoryCommandService _service;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(
            IProductCategoryCommandService service,
            IMapper mapper,
            ILogger<CreateCategoryCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(CreateCategoryCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<ProductCategory>(request.Category);
            var id = await _service.CreateCategoryAsync(entity, ct);
            return new { CategoryId = id };
        }
    }

  
   
    
   

  
}