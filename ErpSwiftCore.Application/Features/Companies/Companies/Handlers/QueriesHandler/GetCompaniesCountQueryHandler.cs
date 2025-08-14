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
    public class GetCompaniesCountQueryHandler : BaseHandler<GetCompaniesCountQuery>
    {
        private readonly ICompanyQueryService _companyQueryService;

        public GetCompaniesCountQueryHandler(
            ICompanyQueryService companyQueryService,
            ILogger<GetCompaniesCountQueryHandler> logger
        ) : base(logger)
        {
            _companyQueryService = companyQueryService;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompaniesCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _companyQueryService.GetCompaniesCountAsync(cancellationToken);
            return new CompanyCountDto { Count = count };
        }
    }

}
