// ملف: ProductCategoryConfiguration.cs
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationProduct
{
    public class ProductCategoryConfiguration
        : AuditableEntityConfiguration<ProductCategory>,
          IEntityTypeConfiguration<ProductCategory>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // علاقة واحد(Parent) → متعدد (no navigation back)
            builder.HasOne(c => c.ParentCategory)
                   .WithMany() // لا توجد خاصية SubCategories بعد الآن
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => new { c.TenantID, c.Name });
        }
    }
}
