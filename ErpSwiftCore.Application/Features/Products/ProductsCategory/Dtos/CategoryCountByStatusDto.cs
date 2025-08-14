using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos
{
    public class CategoryCountByStatusDto
    {
        public int Active { get; set; }
        public int Inactive { get; set; }
        public int SoftDeleted { get; set; }
    }

}
