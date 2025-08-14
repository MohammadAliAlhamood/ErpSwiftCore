using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService;
using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Handlers.CommandsHandler
{
    public class UpdateUnitOfMeasurementCommandHandler : BaseHandler<UpdateUnitOfMeasurementCommand>
    {
        private readonly IUnitOfMeasurementCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateUnitOfMeasurementCommandHandler(
            IUnitOfMeasurementCommandService commandService,
            IMapper mapper,
            ILogger<UpdateUnitOfMeasurementCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateUnitOfMeasurementCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UnitOfMeasurement>(request.Unit);
            var result = await _commandService.UpdateUnitOfMeasurementAsync(entity, cancellationToken);
            return result;
        }
    }
} 