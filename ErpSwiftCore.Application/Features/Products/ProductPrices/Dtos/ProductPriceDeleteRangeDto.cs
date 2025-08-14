using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos
{
    public class ProductPriceDeleteRangeDto
    {
        public IEnumerable<Guid> PriceIds { get; set; }
    }


}
