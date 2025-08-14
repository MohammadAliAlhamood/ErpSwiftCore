using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Companies.Handlers.CommandsHandler
{
    public class UpdateCompanyCommandHandler : BaseHandler<UpdateCompanyCommand>
    {
        private readonly ICompanyCommandService _companyCommandService;
         private readonly IMapper _mapper;
        public UpdateCompanyCommandHandler(
            ICompanyCommandService companyCommandService,
             IMapper mapper,
            ILogger<UpdateCompanyCommandHandler> logger
        ) : base(logger)
        {
            _companyCommandService = companyCommandService; 
            _mapper = mapper;
        }
        protected override async Task<object?> HandleRequestAsync(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
 
            // تحويل DTO إلى Entity مع ضبط المعرف
            var entity = _mapper.Map<SharedKernel.Entities.Company>(request.Company);
             var success = await _companyCommandService.UpdateCompanyAsync(entity, cancellationToken);
            if (!success)
                throw new DomainException($"تعذر تحديث الشركة بالمعرّف '{entity.ID}'.");
             return success;
        }
    }


}
