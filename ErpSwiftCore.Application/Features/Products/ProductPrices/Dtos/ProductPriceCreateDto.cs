using ErpSwiftCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos
{
    public class ProductPriceCreateDto
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public Guid CurrencyId { get; set; }
        public ProductPriceType PriceType { get; set; }
        public DateTime EffectiveDate { get; set; }
    }


}
