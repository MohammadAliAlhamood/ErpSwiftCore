using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Customers.Queries;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using Microsoft.Extensions.Logging;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.QueriesHandler
{
    // Handler
    public class GetAllCustomersQueryHandler   : BaseHandler<GetAllCustomersQuery>
    {
        private readonly ICustomerQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(
            ICustomerQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllCustomersQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAllCustomersQuery req,
            CancellationToken ct)
        {
            var entities = await _svc.GetAllCustomersAsync(ct);
            var dtos = entities
                .Select(e => _mapper.Map<CustomerDto>(e))
                .ToList();
            return dtos;
        }
    }

}
