using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos
{
    public class ProductCategoryBulkDeleteDto
    {
        public IEnumerable<Guid> CategoryIds { get; set; }
    }

}
