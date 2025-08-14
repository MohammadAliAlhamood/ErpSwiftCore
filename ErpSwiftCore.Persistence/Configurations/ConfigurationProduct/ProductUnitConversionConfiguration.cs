using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationProduct
{
    public class ProductUnitConversionConfiguration : AuditableEntityConfiguration<ProductUnitConversion>,IEntityTypeConfiguration<ProductUnitConversion>
    {
        public override void Configure(EntityTypeBuilder<ProductUnitConversion> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.ConversionRate).IsRequired();
            builder.Property(c => c.Factor);
            //builder.HasOne(c => c.Product)
            //       .WithMany(p => p.UnitConversions)
            //       .HasForeignKey(c => c.ProductId);
            builder.HasOne(c => c.FromUnit)
                   .WithMany()
                   .HasForeignKey(c => c.FromUnitId);
            builder.HasOne(c => c.ToUnit)
                   .WithMany()
                   .HasForeignKey(c => c.ToUnitId);
        }
    }
}