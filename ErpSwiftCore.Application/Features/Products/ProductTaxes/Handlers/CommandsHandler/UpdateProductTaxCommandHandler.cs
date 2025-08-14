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
    public class UpdateProductTaxCommandHandler : BaseHandler<UpdateProductTaxCommand>
    {
        private readonly IProductTaxCommandService _svc;
        private readonly IMapper _mapper;
        public UpdateProductTaxCommandHandler(IProductTaxCommandService svc, IMapper mapper, ILogger<UpdateProductTaxCommandHandler> logger)
            : base(logger) { _svc = svc; _mapper = mapper; }

        protected override async Task<object?> HandleRequestAsync(UpdateProductTaxCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.EntityProduct.ProductTax>(request.Tax);
            var ok = await _svc.UpdateTaxAsync(entity, ct);
            return new { Updated = ok };
        }
    }
}
