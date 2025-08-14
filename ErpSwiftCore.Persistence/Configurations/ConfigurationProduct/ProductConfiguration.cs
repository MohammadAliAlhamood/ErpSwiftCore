using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationProduct
{
    public class ProductConfiguration : AuditableEntityConfiguration<Product>,
        IEntityTypeConfiguration<Product> {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(p => p.Code)
                   .HasMaxLength(50);
            builder.Property(p => p.ProductType)
                   .IsRequired();
            builder.Property(p => p.UnitOfMeasurementId)
                   .IsRequired();
            
            builder.HasOne(p => p.UnitOfMeasurement)
                   .WithMany()
                   .HasForeignKey(p => p.UnitOfMeasurementId);
            
            builder.HasOne(p => p.Category)
                   .WithMany()
                   .HasForeignKey(p => p.CategoryId);
        }
    }
}