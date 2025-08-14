using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationProduct
{
    public class ProductTaxConfiguration : AuditableEntityConfiguration<ProductTax>,
        IEntityTypeConfiguration<ProductTax>
    {
        public override void Configure(EntityTypeBuilder<ProductTax> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.Rate).IsRequired();
            //builder.HasOne(t => t.Product)
            //       .WithMany(p => p.Taxes)
            //       .HasForeignKey(t => t.ProductId);
        }
    }
}