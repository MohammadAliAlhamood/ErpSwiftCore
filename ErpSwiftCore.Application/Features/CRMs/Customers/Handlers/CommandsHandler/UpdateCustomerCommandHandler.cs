using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Customers.Commands;
using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.CommandsHandler
{
    public class UpdateCustomerCommandHandler : BaseHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerCommandService _svc;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandHandler(ICustomerCommandService svc, IMapper mapper, ILogger<BaseHandler<UpdateCustomerCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateCustomerCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<Customer>(req.Dto);
            var ok = await _svc.UpdateCustomerAsync(entity, ct);
            return new { Success = ok };
        }
    }

}
