using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Queries
{
    public class GetCurrencyByIdQuery : IRequest<APIResponseDto>
    {
        public Guid CurrencyId { get; }

        public GetCurrencyByIdQuery(Guid currencyId)
        {
            CurrencyId = currencyId;
        }
    }
}