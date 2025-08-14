using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Handlers.CommandsHandler
{
    public class DeleteUnitsOfMeasurementRangeCommandHandler : BaseHandler<DeleteUnitsOfMeasurementRangeCommand>
    {
        private readonly IUnitOfMeasurementCommandService _commandService;

        public DeleteUnitsOfMeasurementRangeCommandHandler(
            IUnitOfMeasurementCommandService commandService,
            ILogger<DeleteUnitsOfMeasurementRangeCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteUnitsOfMeasurementRangeCommand request, CancellationToken cancellationToken)
        {
            return await _commandService.DeleteUnitsOfMeasurementRangeAsync(request.UnitIds, cancellationToken);
        }
    }
} 