using Microsoft.Extensions.Logging; 
using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
namespace ErpSwiftCore.Application.Features.Companies.Companies.Handlers.CommandsHandler
{
    public class RestoreCompanyRangeCommandHandler : BaseHandler<RestoreCompanyRangeCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;

        public RestoreCompanyRangeCommandHandler(ICompanyCommandService companyCommandService, 
            ILogger<BaseHandler<RestoreCompanyRangeCommand>>
            logger) : base(logger)
        {
            _companyCommandService = companyCommandService;
        } 
        protected override async Task<object?> HandleRequestAsync(RestoreCompanyRangeCommand request, CancellationToken cancellationToken)
        {
            // استدعاء خدمة الأوامر لحذف الشركة
            var success = await _companyCommandService.RestoreCompaniesRangeAsync(request.Ids, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"الشركة بالمعرّف   غير موجودة.");
            return new { Message = $"الشركة بالمعرّف   غير موجودة." };
        }
    }   
}
