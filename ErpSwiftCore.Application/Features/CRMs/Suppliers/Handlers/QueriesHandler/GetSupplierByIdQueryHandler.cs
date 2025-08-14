using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Queries;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Handlers.QueriesHandler
{

    public class GetSupplierByIdQueryHandler   : BaseHandler<GetSupplierByIdQuery>
    {
        private readonly ISupplierQueryService _svc;
        private readonly IMapper _mapper;
        public GetSupplierByIdQueryHandler(ISupplierQueryService svc,IMapper mapper,ILogger<BaseHandler<GetSupplierByIdQuery>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        } 
        protected override async Task<object?> HandleRequestAsync(    GetSupplierByIdQuery req,  CancellationToken ct)
        {
            var entity = await _svc.GetSupplierByIdAsync(req.SupplierId, ct);
            return _mapper.Map<SupplierDto?>(entity);
        }
    } 
}
