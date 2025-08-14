using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using ErpSwiftCore.Application.Features.Companies.Currencies.Queries;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Handlers.QueriesHandler
{
    public class GetAllCurrenciesQueryHandler : BaseHandler<GetAllCurrenciesQuery>
    {
        private readonly ICurrencyQueryService _currencyQueryService;
        private readonly IMapper _mapper;

        public GetAllCurrenciesQueryHandler(
            ICurrencyQueryService currencyQueryService,
            IMapper mapper,
            ILogger<GetAllCurrenciesQueryHandler> logger
        ) : base(logger)
        {
            _currencyQueryService = currencyQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _currencyQueryService.GetAllCurrenciesAsync(cancellationToken);
            return _mapper.Map<IReadOnlyList<CurrencyDto>>(entities);
        }
    }
}