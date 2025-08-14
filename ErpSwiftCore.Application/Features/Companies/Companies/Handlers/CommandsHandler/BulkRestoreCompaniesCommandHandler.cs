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
    public class BulkRestoreCompaniesCommandHandler : BaseHandler<BulkRestoreCompaniesCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;
        public BulkRestoreCompaniesCommandHandler(
            ICompanyCommandService companyCommandService,
            ILogger<BulkRestoreCompaniesCommandHandler> logger
        ) : base(logger)
        {
            _companyCommandService = companyCommandService;
        }
        protected override async Task<object?> HandleRequestAsync(BulkRestoreCompaniesCommand request, CancellationToken cancellationToken)
        {
            var restored = new List<Guid>();
            foreach (var id in request.CompanyIds)
            {
                var success = await _companyCommandService.RestoreCompanyAsync(id, cancellationToken);
                if (success) restored.Add(id);
            }
            return new { RestoredCompanyIds = restored };
        }
    }

}
