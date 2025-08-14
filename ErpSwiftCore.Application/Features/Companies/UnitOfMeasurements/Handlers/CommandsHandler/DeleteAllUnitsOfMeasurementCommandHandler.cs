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
    public class DeleteAllUnitsOfMeasurementCommandHandler : BaseHandler<DeleteAllUnitsOfMeasurementCommand>
    {
        private readonly IUnitOfMeasurementCommandService _commandService;

        public DeleteAllUnitsOfMeasurementCommandHandler(
            IUnitOfMeasurementCommandService commandService,
            ILogger<DeleteAllUnitsOfMeasurementCommandHandler> logger
        ) : base(logger)
        {
            _commandService = commandService;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteAllUnitsOfMeasurementCommand request, CancellationToken cancellationToken)
        {
            return await _commandService.DeleteAllUnitsOfMeasurementAsync(cancellationToken);
        }
    }
}