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
    public class GetCompaniesByCountryQueryHandler : BaseHandler<GetCompaniesByCountryQuery>
    {
        private readonly ICompanyQueryService _companyQueryService;
        private readonly IMapper _mapper;

        public GetCompaniesByCountryQueryHandler(
            ICompanyQueryService companyQueryService,
            IMapper mapper,
            ILogger<GetCompaniesByCountryQueryHandler> logger
        ) : base(logger)
        {
            _companyQueryService = companyQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompaniesByCountryQuery request, CancellationToken cancellationToken)
        {
            var entities = await _companyQueryService.GetCompaniesByCountryAsync(request.Country, cancellationToken);
            var dtos = entities.Select(c => _mapper.Map<CompanyDto>(c)).ToList();
            return dtos;
        }
    }

}
