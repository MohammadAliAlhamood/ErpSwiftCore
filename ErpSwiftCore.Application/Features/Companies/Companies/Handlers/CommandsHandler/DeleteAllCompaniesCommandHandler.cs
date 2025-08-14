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
    public class DeleteAllCompaniesCommandHandler : BaseHandler<DeleteAllCompaniesCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;

        public DeleteAllCompaniesCommandHandler(
            ICompanyCommandService companyCommandService,
            ILogger<DeleteAllCompaniesCommandHandler> logger
        ) : base(logger)
        {
            _companyCommandService = companyCommandService;
        }
        protected override async Task<object?> HandleRequestAsync(DeleteAllCompaniesCommand request, CancellationToken cancellationToken)
        {
            var success = await _companyCommandService.DeleteAllCompaniesAsync(cancellationToken);
            if (!success)
                throw new DomainException("فشل حذف جميع الشركات.");
            return new { Message = "تمّ حذف جميع الشركات بنجاح." };
        }
    }


}
