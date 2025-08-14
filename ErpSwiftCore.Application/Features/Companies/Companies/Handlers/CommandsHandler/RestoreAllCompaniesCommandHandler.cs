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
    public class RestoreAllCompaniesCommandHandler : BaseHandler<RestoreAllCompaniesCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;
        public RestoreAllCompaniesCommandHandler(
            ICompanyCommandService companyCommandService,
            ILogger<RestoreAllCompaniesCommandHandler> logger
        ) : base(logger)
        {
            _companyCommandService = companyCommandService;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreAllCompaniesCommand request, CancellationToken cancellationToken)
        {
            var success = await _companyCommandService.RestoreAllCompaniesAsync(cancellationToken);
            if (!success)
                throw new DomainException("فشل استرجاع جميع الشركات.");
            return new { Message = "تمّ استرجاع جميع الشركات بنجاح." };
        }
    }

}
