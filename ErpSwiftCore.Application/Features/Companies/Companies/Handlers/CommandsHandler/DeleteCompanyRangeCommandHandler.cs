using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Companies.Companies.Handlers.CommandsHandler
{
    public class DeleteCompanyRangeCommandHandler : BaseHandler<DeleteCompanyRangeCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;

        public DeleteCompanyRangeCommandHandler(ICompanyCommandService companyCommandService, ILogger<BaseHandler<DeleteCompanyRangeCommand>> logger) : base(logger)
        {
            _companyCommandService = companyCommandService;
        }
 
        protected override async Task<object?> HandleRequestAsync(DeleteCompanyRangeCommand request, CancellationToken cancellationToken)
        {
            // استدعاء خدمة الأوامر لحذف الشركة
            var success = await _companyCommandService.DeleteCompaniesRangeAsync(request.Ids, cancellationToken);
            if (!success) throw new DomainNotFoundException($"الشركة بالمعرّف  غير موجودة.");
            return new { Message = $"الشركة بالمعرّف  غير موجودة." };
        }
    }
}
