using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Handlers.CommandsHandler
{
    public class BulkCreateCompaniesCommandHandler : BaseHandler<BulkCreateCompaniesCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;
        private readonly IMapper _mapper;
        public BulkCreateCompaniesCommandHandler(
            ICompanyCommandService companyCommandService,
            IMapper mapper,
            ILogger<BulkCreateCompaniesCommandHandler> logger
        ) : base(logger)
        {
            _companyCommandService = companyCommandService;
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(BulkCreateCompaniesCommand request, CancellationToken cancellationToken)
        {
            // تحويل قائمة DTOs إلى Entities
            var entities = request.Companies.Select(dto => _mapper.Map<SharedKernel.Entities.Company>(dto));
            // استدعاء خدمة الأوامر لإضافة مجموعة الشركات
            var newIds = await _companyCommandService.AddCompaniesRangeAsync(entities, cancellationToken);
            return new { CreatedCompanyIds = newIds.ToList() };
        }
    }

}
