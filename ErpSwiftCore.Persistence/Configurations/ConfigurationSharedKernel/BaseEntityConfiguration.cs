using ErpSwiftCore.SharedKernel.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel
{
    public class BaseEntityConfiguration<T>
       : IEntityTypeConfiguration<T>
       where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // 🔑 المفتاح الأساسي
            builder.HasKey(e => e.ID);

            builder.Property(e => e.ID)
                   .ValueGeneratedOnAdd();

            // 🕒 التواريخ
            builder.Property(e => e.CreatedAt)
                   .IsRequired();

            builder.Property(e => e.UpdatedAt);

            // 👤 المستخدمون
            builder.Property(e => e.CreatedBy)
                   .IsRequired();

            builder.Property(e => e.UpdatedBy);

            // 🗑 الحذف المنطقي
            builder.Property(e => e.IsDeleted)
                   .HasDefaultValue(false)
                   .IsRequired();

            // ✅ فهارس تسريعية لتحسين الأداء
            builder.HasIndex(e => e.CreatedAt)
                   .HasDatabaseName("IX_Entity_CreatedAt");

            builder.HasIndex(e => e.CreatedBy)
                   .HasDatabaseName("IX_Entity_CreatedBy");

            builder.HasIndex(e => e.IsDeleted)
                   .HasDatabaseName("IX_Entity_IsDeleted");
        }
    }
}