using ErpSwiftCore.Application.Features.Companies.Currencies.Commands;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Handlers.CommandsHandler
{
    public class DeleteCurrencyCommandHandler : BaseHandler<DeleteCurrencyCommand>
    {
        private readonly ICurrencyCommandService _currencyCommandService;

        public DeleteCurrencyCommandHandler(
            ICurrencyCommandService currencyCommandService,
            ILogger<DeleteCurrencyCommandHandler> logger
        ) : base(logger)
        {
            _currencyCommandService = currencyCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteCurrencyCommand request, CancellationToken cancellationToken)
        {
            return await _currencyCommandService.DeleteCurrencyAsync(request.CurrencyId, cancellationToken);
        }
    }
}