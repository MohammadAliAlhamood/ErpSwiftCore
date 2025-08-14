using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Companies.Companies.Handlers.CommandsHandler
{

    public class CreateCompanyCommandHandler : BaseHandler<CreateCompanyCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;
         private readonly IMapper _mapper;
        public CreateCompanyCommandHandler(
            ICompanyCommandService companyCommandService,
            ICompanyQueryService companyQueryService,
            IMapper mapper,
            ILogger<CreateCompanyCommandHandler> logger
        ) : base(logger)
        {
            _companyCommandService = companyCommandService;
             _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map< Company>(request.Company);
            return await _companyCommandService.CreateCompanyAsync(entity, cancellationToken);
        }
    }
}