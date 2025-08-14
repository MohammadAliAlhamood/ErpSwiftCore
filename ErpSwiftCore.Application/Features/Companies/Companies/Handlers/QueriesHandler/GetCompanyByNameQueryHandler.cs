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
    public class GetCompanyByNameQueryHandler : BaseHandler<GetCompanyByNameQuery>
    {
        private readonly ICompanyQueryService _companyQueryService;
        private readonly IMapper _mapper;

        public GetCompanyByNameQueryHandler(
            ICompanyQueryService companyQueryService,
            IMapper mapper,
            ILogger<GetCompanyByNameQueryHandler> logger
        ) : base(logger)
        {
            _companyQueryService = companyQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompanyByNameQuery request, CancellationToken cancellationToken)
        {
            var entity = await _companyQueryService.GetCompanyByNameAsync(request.CompanyName, cancellationToken);
            if (entity == null)
                throw new DomainNotFoundException($"الشركة بالاسم '{request.CompanyName}' غير موجودة.");
            return _mapper.Map<CompanyDto>(entity);
        }
    }

}
