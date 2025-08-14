using AutoMapper;
using ErpSwiftCore.Application.Features.CRMs.Suppliers.Commands;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Commands;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Dtos;
using ErpSwiftCore.Application.Features.CRMs.Supplies.Queries;
using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Supplies.Handlers.CommandsHandler
{
    public class CreateSupplierCommandHandler : BaseHandler<CreateSupplierCommand>
    {
        private readonly ISupplierCommandService _svc;
        private readonly IMapper _mapper;
        public CreateSupplierCommandHandler(ISupplierCommandService svc, IMapper mapper, ILogger<BaseHandler<CreateSupplierCommand>> logger) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(CreateSupplierCommand req, CancellationToken ct)
        {
            var entity = _mapper.Map<Supplier>(req.Dto);
            var id = await _svc.CreateSupplierAsync(entity, ct);
            return new { Id = id };
        }
    }




 
  







}
