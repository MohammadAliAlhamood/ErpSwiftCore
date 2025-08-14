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
    public class RestoreAllProductUnitConversionsCommandHandler
     : BaseHandler<RestoreAllProductUnitConversionsCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public RestoreAllProductUnitConversionsCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<RestoreAllProductUnitConversionsCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreAllProductUnitConversionsCommand request,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreAllUnitConversionsAsync(ct);
            return new { RestoredAll = ok };
        }
    }


}
