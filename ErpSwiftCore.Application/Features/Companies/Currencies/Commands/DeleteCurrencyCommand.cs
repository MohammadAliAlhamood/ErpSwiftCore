using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Commands
{
    public class DeleteCurrencyCommand : IRequest<APIResponseDto>
    {
        public Guid CurrencyId { get; }

        public DeleteCurrencyCommand(Guid currencyId)
        {
            CurrencyId = currencyId;
        }
    }
}