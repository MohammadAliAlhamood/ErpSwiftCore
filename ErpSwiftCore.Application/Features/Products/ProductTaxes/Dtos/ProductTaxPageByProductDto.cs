using ErpSwiftCore.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos
{
    public class ProductTaxPageByProductDto : BasePageParamDto
    {
        public Guid ProductId { get; set; }
    }

}
