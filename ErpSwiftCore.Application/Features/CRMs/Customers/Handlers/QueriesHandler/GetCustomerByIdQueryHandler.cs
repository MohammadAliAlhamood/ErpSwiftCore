using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Customers.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Customers.Queries;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using Microsoft.Extensions.Logging;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.QueriesHandler
{

      public class GetCustomerByIdQueryHandler
       : BaseHandler<GetCustomerByIdQuery>
    {
        private readonly ICustomerQueryService _svc;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(
            ICustomerQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetCustomerByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetCustomerByIdQuery req,
            CancellationToken ct)
        {
            var entity = await _svc.GetCustomerByIdAsync(req.CustomerId, ct);
            return _mapper.Map<CustomerDto?>(entity);
        }
    }


}
