using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos
{
    public class ProductUnitConversionUpdateDto : ProductUnitConversionCreateDto
    {
        public Guid Id { get; set; }
    }


}
