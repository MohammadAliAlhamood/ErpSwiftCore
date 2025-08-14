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
    public class SoftDeleteCompanyCommandHandler : BaseHandler<SoftDeleteCompanyCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;

        public SoftDeleteCompanyCommandHandler(
             ICompanyCommandService companyCommandService,
            ILogger<BaseHandler<SoftDeleteCompanyCommand>> logger) : base(logger)
        {
            _companyCommandService = companyCommandService;
        }
         
        protected override async Task<object?> HandleRequestAsync(SoftDeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            // استدعاء خدمة الأوامر لحذف الشركة
            var success = await _companyCommandService.SoftDeleteCompanyAsync(request.CompanyId, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"الشركة بالمعرّف '{request.CompanyId}' غير موجودة.");
            return new { Message = $"تمّ حذف الشركة '{request.CompanyId}' بنجاح." };
        }
    }


}
