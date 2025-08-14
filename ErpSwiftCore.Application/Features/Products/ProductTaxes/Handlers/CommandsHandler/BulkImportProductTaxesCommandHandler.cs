using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Handlers.CommandsHandler
{
    public class BulkImportProductTaxesCommandHandler : BaseHandler<BulkImportProductTaxesCommand>
    {
        private readonly IProductTaxCommandService _svc;
        private readonly IMapper _mapper;
        public BulkImportProductTaxesCommandHandler(IProductTaxCommandService svc, IMapper mapper, ILogger<BulkImportProductTaxesCommandHandler> logger)
            : base(logger) { _svc = svc; _mapper = mapper; }

        protected override async Task<object?> HandleRequestAsync(BulkImportProductTaxesCommand request, CancellationToken ct)
        {
            var entities = request.Taxes.Select(t => _mapper.Map< ProductTax>(t));
            var count = await _svc.BulkImportTaxesAsync(entities, ct);
            return new { ImportedCount = count };
        }
    }
}
