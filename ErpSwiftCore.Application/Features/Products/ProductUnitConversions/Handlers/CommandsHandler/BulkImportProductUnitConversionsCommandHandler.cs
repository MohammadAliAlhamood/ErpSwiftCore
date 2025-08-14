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

    public class BulkImportProductUnitConversionsCommandHandler
        : BaseHandler<BulkImportProductUnitConversionsCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        private readonly IMapper _mapper;
        public BulkImportProductUnitConversionsCommandHandler(
            IProductUnitConversionCommandService svc,
            IMapper mapper,
            ILogger<BulkImportProductUnitConversionsCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkImportProductUnitConversionsCommand request,
            CancellationToken ct)
        {
            var entities = request.Dtos
                .Select(d => _mapper.Map<ProductUnitConversion>(d));
            var count = await _svc.BulkImportUnitConversionsAsync(entities, ct);
            return new { ImportedCount = count };
        }
    }


}
