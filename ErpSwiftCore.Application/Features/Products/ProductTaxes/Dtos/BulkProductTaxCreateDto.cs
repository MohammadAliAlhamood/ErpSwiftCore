using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos
{
    // Bulk Operations
    public class BulkProductTaxCreateDto
    {
        public IEnumerable<ProductTaxCreateDto> Taxes { get; set; }
    }
}
