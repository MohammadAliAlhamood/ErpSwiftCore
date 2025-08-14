using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Handlers.CommandsHandler
{

    public class DeleteProductUnitConversionsRangeCommandHandler
        : BaseHandler<DeleteProductUnitConversionsRangeCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public DeleteProductUnitConversionsRangeCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<DeleteProductUnitConversionsRangeCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteProductUnitConversionsRangeCommand request,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteUnitConversionsRangeAsync(request.ConversionIds, ct);
            return new { DeletedCount = ok };
        }
    }

}
