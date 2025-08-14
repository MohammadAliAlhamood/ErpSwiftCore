using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ErpSwiftCore.Persistence.Configurations.ConfigurationProduct
{
    public class ProductBundleConfiguration : AuditableEntityConfiguration<ProductBundle>,
        IEntityTypeConfiguration<ProductBundle>
    {
        public override void Configure(EntityTypeBuilder<ProductBundle> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Quantity).IsRequired();
            builder.HasOne(b => b.ComponentProduct)
                   .WithMany()
                   .HasForeignKey(b => b.ComponentProductId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.UnitOfMeasurement)
                   .WithMany()
                   .HasForeignKey(b => b.UnitOfMeasurementId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}