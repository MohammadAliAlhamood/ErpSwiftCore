using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Handlers.CommandsHandler
{
    public class UpdateProductUnitConversionCommandHandler
       : BaseHandler<UpdateProductUnitConversionCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        private readonly IMapper _mapper;
        public UpdateProductUnitConversionCommandHandler(
            IProductUnitConversionCommandService svc,
            IMapper mapper,
            ILogger<UpdateProductUnitConversionCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateProductUnitConversionCommand request,
            CancellationToken ct)
        {
            var entity = _mapper.Map<ProductUnitConversion>(request.Dto);
            var ok = await _svc.UpdateUnitConversionAsync(entity, ct);
            return new { Updated = ok };
        }
    }

}
