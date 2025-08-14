using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Handlers.CommandsHandler
{
    public class BulkCreateProductTaxesCommandHandler : BaseHandler<BulkCreateProductTaxesCommand>
    {
        private readonly IProductTaxCommandService _svc;
        private readonly IMapper _mapper;
        public BulkCreateProductTaxesCommandHandler(IProductTaxCommandService svc, IMapper mapper, ILogger<BulkCreateProductTaxesCommandHandler> logger)
            : base(logger) { _svc = svc; _mapper = mapper; }

        protected override async Task<object?> HandleRequestAsync(BulkCreateProductTaxesCommand request, CancellationToken ct)
        {
            var list = request.Taxes.Select(t => _mapper.Map<Domain.Entities.EntityProduct.ProductTax>(t));
            var ids = await _svc.AddTaxesRangeAsync(list, ct);
            return new { CreatedTaxIds = ids };
        }
    }
}
