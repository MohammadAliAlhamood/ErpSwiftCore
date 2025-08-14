using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.Currencies.Commands
{
    public class CreateCurrencyCommand : IRequest<APIResponseDto>
    {
        public CurrencyCreateDto Currency { get; }

        public CreateCurrencyCommand(CurrencyCreateDto currency)
        {
            Currency = currency;
        }
    }
}