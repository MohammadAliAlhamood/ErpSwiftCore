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
    public class UpdateCategoryCommandHandler : BaseHandler<UpdateCategoryCommand>
    {
        private readonly IProductCategoryCommandService _service;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(
            IProductCategoryCommandService service,
            IMapper mapper,
            ILogger<UpdateCategoryCommandHandler> logger
        ) : base(logger)
        {
            _service = service;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateCategoryCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<ProductCategory>(request.Category);
            var success = await _service.UpdateCategoryAsync(entity, ct);
            return new { Success = success };
        }
    }

}
