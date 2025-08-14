using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
namespace ErpSwiftCore.Application.Features.Companies.Companies.Handlers.CommandsHandler
{
    public class SoftDeleteCompanyRangeCommandHandler : BaseHandler<SoftDeleteCompanyCommand>
    {
        private readonly ICompanyCommandService _companyCommandService; 
        public SoftDeleteCompanyRangeCommandHandler(ICompanyCommandService companyCommandService, ILogger<BaseHandler<SoftDeleteCompanyCommand>> logger) : base(logger)
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
