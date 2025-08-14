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

    public class GetAllProductsQueryHandler : BaseHandler<GetAllProductsQuery>
    {
        private readonly IProductQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(
            IProductQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllProductsQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetAllProductsQuery req, CancellationToken ct)
        {
            var list = await _svc.GetAllProductsAsync(ct);
            return list.Select(e => _mapper.Map<ProductDto>(e));
        }
    }




}
