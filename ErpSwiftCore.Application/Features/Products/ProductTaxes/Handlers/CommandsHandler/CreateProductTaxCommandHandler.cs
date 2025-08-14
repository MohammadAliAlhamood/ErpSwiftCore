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
    public class CreateProductTaxCommandHandler : BaseHandler<CreateProductTaxCommand>
    {
        private readonly IProductTaxCommandService _svc;
        private readonly IMapper _mapper;
        public CreateProductTaxCommandHandler(IProductTaxCommandService svc, IMapper mapper, ILogger<CreateProductTaxCommandHandler> logger)
            : base(logger) { _svc = svc; _mapper = mapper; }

        protected override async Task<object?> HandleRequestAsync(CreateProductTaxCommand request, CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.EntityProduct.ProductTax>(request.Tax);
            var id = await _svc.CreateTaxAsync(entity, ct);
            return new { CreatedTaxId = id };
        }
    }

 

   


}