using AutoMapper;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.Product.Handlers.QueriesHandler
{

    public class GetProductByCodeQueryHandler : BaseHandler<GetProductByCodeQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;

        public GetProductByCodeQueryHandler(
            IProductQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetProductByCodeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetProductByCodeQuery req, CancellationToken ct)
        {
            var e = await _svc.GetProductByCodeAsync(req.Code, ct);
            return _mapper.Map<ProductDto?>(e);
        }
    }



}
