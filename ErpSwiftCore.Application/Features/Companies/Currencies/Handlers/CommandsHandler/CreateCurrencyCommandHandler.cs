using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.Currencies.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService;
using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Handlers.CommandsHandler
{
    public class CreateCurrencyCommandHandler : BaseHandler<CreateCurrencyCommand>
    {
        private readonly ICurrencyCommandService _currencyCommandService;
        private readonly IMapper _mapper;

        public CreateCurrencyCommandHandler(
            ICurrencyCommandService currencyCommandService,
            IMapper mapper,
            ILogger<CreateCurrencyCommandHandler> logger
        ) : base(logger)
        {
            _currencyCommandService = currencyCommandService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(CreateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Currency>(request.Currency);
            return await _currencyCommandService.CreateCurrencyAsync(entity, cancellationToken);
        }
    }
}