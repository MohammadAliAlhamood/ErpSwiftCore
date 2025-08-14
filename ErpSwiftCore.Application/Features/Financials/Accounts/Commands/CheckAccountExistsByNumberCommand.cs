using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.Accounts.Commands
{
    public class CheckAccountExistsByNumberCommand : IRequest<APIResponseDto>
    {
        public string AccountNumber { get; }
        public CheckAccountExistsByNumberCommand(string accountNumber) => AccountNumber = accountNumber;
    }
}
