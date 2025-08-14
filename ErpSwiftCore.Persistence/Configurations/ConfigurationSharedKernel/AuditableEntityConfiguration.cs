using ErpSwiftCore.SharedKernel.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel
{
    public class AuditableEntityConfiguration<T>  : BaseEntityConfiguration<T> 
        where T : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            // أولًا: طبق إعدادات BaseEntity
            base.Configure(builder);

            // ثانيًا: أضف إعدادات AuditableEntity
            builder.HasOne(e => e.Company)
                   .WithMany()
                   .HasForeignKey(e => e.TenantID);

            // ثالثًا: تأكد أن TenantID غير قابل لـ null
            builder.Property(e => e.TenantID)
                   .IsRequired();
            builder.HasIndex(e => e.TenantID)
                   .HasDatabaseName("IX_TenantID");
        }
    }
}