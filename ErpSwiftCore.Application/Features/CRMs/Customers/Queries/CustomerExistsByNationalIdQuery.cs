using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.CRMs.Customers.Queries
{
    // 5. קיום לפי ת.ז. (יצירה)
    public class CustomerExistsByNationalIdQuery : IRequest<APIResponseDto>
    {
        public string NationalId { get; }
        public CustomerExistsByNationalIdQuery(string nationalId) => NationalId = nationalId;
    }
}
