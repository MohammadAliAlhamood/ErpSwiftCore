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
    public class BulkDeleteCompaniesCommandHandler : BaseHandler<BulkDeleteCompaniesCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;

        public BulkDeleteCompaniesCommandHandler(
            ICompanyCommandService companyCommandService,
            ILogger<BulkDeleteCompaniesCommandHandler> logger
        ) : base(logger)
        {
            _companyCommandService = companyCommandService;
        }
        protected override async Task<object?> HandleRequestAsync(BulkDeleteCompaniesCommand request, CancellationToken cancellationToken)
        {
            var successes = new List<Guid>();
            foreach (var id in request.CompanyIds)
            {
                var deleted = await _companyCommandService.DeleteCompanyAsync(id, cancellationToken);
                if (deleted) successes.Add(id);
            }
            return new { DeletedCompanyIds = successes };
        }
    }

}
