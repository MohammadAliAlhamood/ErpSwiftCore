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
    public class GetCurrencyByIdQueryHandler : BaseHandler<GetCurrencyByIdQuery>
    {
        private readonly ICurrencyQueryService _currencyQueryService;
        private readonly IMapper _mapper;

        public GetCurrencyByIdQueryHandler(
            ICurrencyQueryService currencyQueryService,
            IMapper mapper,
            ILogger<GetCurrencyByIdQueryHandler> logger
        ) : base(logger)
        {
            _currencyQueryService = currencyQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _currencyQueryService.GetCurrencyByIdAsync(request.CurrencyId, cancellationToken);
            if (entity == null)
                throw new DomainNotFoundException("العملة المطلوبة غير موجودة.");

            return _mapper.Map<CurrencyDto>(entity);
        }
    }
}