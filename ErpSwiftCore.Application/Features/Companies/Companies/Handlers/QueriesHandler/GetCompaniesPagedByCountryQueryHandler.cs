using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using ErpSwiftCore.Application.Features.Companies.Companies.Queries;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Handlers.QueriesHandler
{
    public class GetCompaniesPagedByCountryQueryHandler : BaseHandler<GetCompaniesPagedByCountryQuery>
    {
        private readonly ICompanyQueryService _companyQueryService;
        private readonly IMapper _mapper;

        public GetCompaniesPagedByCountryQueryHandler(
            ICompanyQueryService companyQueryService,
            IMapper mapper,
            ILogger<GetCompaniesPagedByCountryQueryHandler> logger
        ) : base(logger)
        {
            _companyQueryService = companyQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompaniesPagedByCountryQuery request, CancellationToken cancellationToken)
        {
            var (entities, total) = await _companyQueryService.GetCompaniesPagedByCountryAsync(request.Country, request.PageIndex, request.PageSize, cancellationToken);
            var dtos = entities.Select(c => _mapper.Map<CompanyDto>(c)).ToList();
            return new CompanyPagedResultDto
            {
                Companies = dtos,
                TotalCount = total
            };
        }
    }

}
