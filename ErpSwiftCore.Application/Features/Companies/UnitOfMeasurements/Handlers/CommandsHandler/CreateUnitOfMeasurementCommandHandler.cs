using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService;
using ErpSwiftCore.SharedKernel.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Handlers.CommandsHandler
{
    public class CreateUnitOfMeasurementCommandHandler : BaseHandler<CreateUnitOfMeasurementCommand>
    {
        private readonly IUnitOfMeasurementCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateUnitOfMeasurementCommandHandler(
            IUnitOfMeasurementCommandService commandService,
            IMapper mapper,
            ILogger<CreateUnitOfMeasurementCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(CreateUnitOfMeasurementCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UnitOfMeasurement>(request.Unit);
            return await _commandService.CreateUnitOfMeasurementAsync(entity, cancellationToken);
        }
    }
}
