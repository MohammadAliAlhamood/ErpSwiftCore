using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Queries;
using ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Handlers.QueriesHandler
{
    public class GetAllUnitsOfMeasurementQueryHandler : BaseHandler<GetAllUnitsOfMeasurementQuery>
    {
        private readonly IUnitOfMeasurementQueryService _queryService;
        private readonly IMapper _mapper;

        public GetAllUnitsOfMeasurementQueryHandler(
            IUnitOfMeasurementQueryService queryService,
            IMapper mapper,
            ILogger<GetAllUnitsOfMeasurementQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetAllUnitsOfMeasurementQuery request, CancellationToken cancellationToken)
        {
            var entities = await _queryService.GetAllUnitsOfMeasurementAsync(cancellationToken);
            return _mapper.Map<IReadOnlyList<UnitOfMeasurementDto>>(entities);
        }
    }
}