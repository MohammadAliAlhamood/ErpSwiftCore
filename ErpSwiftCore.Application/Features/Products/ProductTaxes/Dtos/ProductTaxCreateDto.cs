using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos
{
    public class ProductTaxCreateDto
    {
        public Guid ProductId { get; set; }
        public decimal Rate { get; set; }
    }

}
