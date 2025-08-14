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

    public class DeleteAllProductUnitConversionsCommandHandler
        : BaseHandler<DeleteAllProductUnitConversionsCommand>
    {
        private readonly IProductUnitConversionCommandService _svc;
        public DeleteAllProductUnitConversionsCommandHandler(
            IProductUnitConversionCommandService svc,
            ILogger<DeleteAllProductUnitConversionsCommandHandler> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteAllProductUnitConversionsCommand request,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAllUnitConversionsAsync(ct);
            return new { DeletedAll = ok };
        }
    }

}
