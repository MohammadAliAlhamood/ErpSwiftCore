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
    public class GetCompaniesPagedQueryHandler : BaseHandler<GetCompaniesPagedQuery>
    {
        private readonly ICompanyQueryService _companyQueryService;
        private readonly IMapper _mapper;

        public GetCompaniesPagedQueryHandler(
            ICompanyQueryService companyQueryService,
            IMapper mapper,
            ILogger<GetCompaniesPagedQueryHandler> logger
        ) : base(logger)
        {
            _companyQueryService = companyQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompaniesPagedQuery request, CancellationToken cancellationToken)
        {
            var (entities, total) = await _companyQueryService.GetCompaniesPagedAsync(request.PageIndex, request.PageSize, cancellationToken);
            var dtos = entities.Select(c => _mapper.Map<CompanyDto>(c)).ToList();
            return new CompanyPagedResultDto
            {
                Companies = dtos,
                TotalCount = total
            };
        }
    }

}
