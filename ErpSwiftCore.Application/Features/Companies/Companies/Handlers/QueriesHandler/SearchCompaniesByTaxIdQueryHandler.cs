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
    public class SearchCompaniesByTaxIdQueryHandler : BaseHandler<SearchCompaniesByTaxIdQuery>
    {
        private readonly ICompanyQueryService _companyQueryService;
        private readonly IMapper _mapper;

        public SearchCompaniesByTaxIdQueryHandler(
            ICompanyQueryService companyQueryService,
            IMapper mapper,
            ILogger<SearchCompaniesByTaxIdQueryHandler> logger
        ) : base(logger)
        {
            _companyQueryService = companyQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(SearchCompaniesByTaxIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _companyQueryService.SearchCompaniesByTaxIdAsync(request.TaxId, cancellationToken);
            var dtos = entities.Select(c => _mapper.Map<CompanyDto>(c)).ToList();
            return dtos;
        }
    }

}
