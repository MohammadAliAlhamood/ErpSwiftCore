using AutoMapper;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Handlers.QueriesHandler
{

    public class GetAllProductTaxesQueryHandler : BaseHandler<GetAllProductTaxesQuery>
    {
        private readonly IProductTaxQueryService _svc;
        private readonly IMapper _mapper;
        public GetAllProductTaxesQueryHandler(IProductTaxQueryService svc, IMapper mapper, ILogger<GetAllProductTaxesQueryHandler> logger)
            : base(logger) { _svc = svc; _mapper = mapper; }

        protected override async Task<object?> HandleRequestAsync(GetAllProductTaxesQuery request, CancellationToken ct)
        {
            var list = await _svc.GetAllTaxesAsync(ct);
            return list.Select(e => _mapper.Map<ProductTaxDto>(e)).ToList();
        }
    }

}
