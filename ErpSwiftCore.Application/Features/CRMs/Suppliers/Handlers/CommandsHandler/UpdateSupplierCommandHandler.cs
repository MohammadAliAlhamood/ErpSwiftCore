using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands;
using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Handlers.CommandsHandler
{
    public class UpdateSupplierCommandHandler : BaseHandler<UpdateSupplierCommand>
    {
        private readonly ISupplierCommandService _svc;
        private readonly IMapper _mapper;
        public UpdateSupplierCommandHandler(ISupplierCommandService svc, IMapper mapper, ILogger<BaseHandler<UpdateSupplierCommand>> logger) : base(logger)
        {
            _mapper = mapper;
            _svc = svc;
        }
        protected override async Task<object?> HandleRequestAsync(UpdateSupplierCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<Supplier>(req.Dto);
            var ok = await _svc.UpdateSupplierAsync(entity, ct);
            return new { Success = ok };
        }
    }
}
