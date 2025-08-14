using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using Microsoft.Extensions.Logging;
namespace ErpSwiftCore.Application.Features.Companies.Companies.Handlers.CommandsHandler
{
    public class DeleteCompanyCommandHandler : BaseHandler<DeleteCompanyCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;

        public DeleteCompanyCommandHandler(ICompanyCommandService companyCommandService, ILogger<BaseHandler<DeleteCompanyCommand>> logger)
              : base(logger)
        {
              _companyCommandService = companyCommandService;
        } 
        protected override async Task<object?> HandleRequestAsync(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            // استدعاء خدمة الأوامر لحذف الشركة
            var success = await _companyCommandService.DeleteCompanyAsync(request.Id, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"الشركة بالمعرّف   غير موجودة.");
            return new { Message = $"الشركة بالمعرّف   غير موجودة." };
        }
    }
}
