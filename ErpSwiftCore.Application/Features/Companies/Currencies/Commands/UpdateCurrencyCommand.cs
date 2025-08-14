using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Commands
{
    public class UpdateCurrencyCommand : IRequest<APIResponseDto>
    {
        public CurrencyUpdateDto Currency { get; }

        public UpdateCurrencyCommand(CurrencyUpdateDto currency)
        {
            Currency = currency;
        }
    }
}