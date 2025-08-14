using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos
{
    public class ProductTaxPageDto
    {
        public IReadOnlyList<ProductTaxDto> Taxes { get; set; }
        public int TotalCount { get; set; }
    }


}
