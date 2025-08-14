using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Customers.Commands;
using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.CRMs.Customers.Handlers.CommandsHandler
{
    public class CreateCustomerCommandHandler : BaseHandler<CreateCustomerCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICustomerCommandService _svc;
        public CreateCustomerCommandHandler(ICustomerCommandService svc, IMapper mapper, ILogger<BaseHandler<CreateCustomerCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(
            CreateCustomerCommand req,
            CancellationToken ct)
        {
            var entity = _mapper.Map<Customer>(req.Dto);
            var id = await _svc.CreateCustomerAsync(entity, ct);
            return new { Id = id };
        }
    }
}