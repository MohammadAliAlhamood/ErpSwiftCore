using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos
{
    public class ProductBundleWithRelationsDto : AuditableEntityDto
    {
        public Guid ParentProductId { get; set; }
        public ProductDto? ParentProduct { get; set; }

        public Guid ComponentProductId { get; set; }
        public ProductDto? ComponentProduct { get; set; }

        public decimal Quantity { get; set; }

        public Guid? UnitOfMeasurementId { get; set; }
        public UnitOfMeasurementDto? UnitOfMeasurement { get; set; }
    }


}
