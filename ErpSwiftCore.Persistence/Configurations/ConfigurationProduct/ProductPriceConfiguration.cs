using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationProduct
{
    public class ProductPriceConfiguration : AuditableEntityConfiguration<ProductPrice>,
         IEntityTypeConfiguration<ProductPrice>
    {
        public override void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Price)
                   .IsRequired();

            builder.Property(p => p.PriceType)
                   .IsRequired()
                   .HasConversion<int>();  

            builder.Property(p => p.EffectiveDate)
                   .IsRequired();

            builder.HasOne(p => p.Currency)
                   .WithMany()
                   .HasForeignKey(p => p.CurrencyId);
        }
    }
} 