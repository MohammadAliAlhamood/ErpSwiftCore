using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{
    public class CustomerExistsByNationalIdExcludingQuery : IRequest<APIResponseDto>
    {
        public string NationalId { get; }
        public Guid ExcludingId { get; }
        public CustomerExistsByNationalIdExcludingQuery(string nationalId, Guid excludingId)
        {
            NationalId = nationalId;
            ExcludingId = excludingId;
        }
    }

}
