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
    public class RestoreCompanyCommandHandler : BaseHandler<RestoreCompanyCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;
        public RestoreCompanyCommandHandler(
            ICompanyCommandService companyCommandService,
            ILogger<RestoreCompanyCommandHandler> logger
        ) : base(logger)
        {
            _companyCommandService = companyCommandService;
        }
        protected override async Task<object?> HandleRequestAsync(RestoreCompanyCommand request, CancellationToken cancellationToken)
        {
            var success = await _companyCommandService.RestoreCompanyAsync(request.CompanyId, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"الشركة بالمعرّف '{request.CompanyId}' غير موجودة أو غير مؤرشفة.");
            return new { Message = $"تمّ استرجاع الشركة '{request.CompanyId}' بنجاح." };
        }
    }

}
