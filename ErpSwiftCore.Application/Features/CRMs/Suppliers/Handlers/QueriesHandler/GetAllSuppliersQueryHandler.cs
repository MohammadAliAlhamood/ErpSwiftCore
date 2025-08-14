using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Suppliers.Queries;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Handlers.QueriesHandler
{
    public class GetAllSuppliersQueryHandler : BaseHandler<GetAllSuppliersQuery>
    {
        private readonly ISupplierQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllSuppliersQueryHandler(
            ISupplierQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllSuppliersQuery>> logger)
            : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        } 
        protected override async Task<object?> HandleRequestAsync(  GetAllSuppliersQuery req,   CancellationToken ct)
        { 
            var entities = await _svc.GetAllSuppliersAsync(ct);
            return _mapper.Map<IReadOnlyList<SupplierDto>>(entities);
        }
    }
}