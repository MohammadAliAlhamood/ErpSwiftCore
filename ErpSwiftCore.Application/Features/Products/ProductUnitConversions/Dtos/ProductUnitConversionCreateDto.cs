using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos
{
    public class ProductUnitConversionCreateDto
    {
        public Guid ProductId { get; set; }
        public Guid FromUnitId { get; set; }
        public Guid ToUnitId { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal Factor { get; set; }
    }
}
