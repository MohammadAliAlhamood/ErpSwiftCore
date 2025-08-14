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
    public class GetUnitOfMeasurementByIdQueryHandler : BaseHandler<GetUnitOfMeasurementByIdQuery>
    {
        private readonly IUnitOfMeasurementQueryService _queryService;
        private readonly IMapper _mapper;

        public GetUnitOfMeasurementByIdQueryHandler(
            IUnitOfMeasurementQueryService queryService,
            IMapper mapper,
            ILogger<GetUnitOfMeasurementByIdQueryHandler> logger
        ) : base(logger)
        {
            _queryService = queryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetUnitOfMeasurementByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _queryService.GetUnitOfMeasurementByIdAsync(request.UnitId, cancellationToken);
            if (entity == null) return null;

            return _mapper.Map<UnitOfMeasurementDto>(entity);
        }
    }
    }