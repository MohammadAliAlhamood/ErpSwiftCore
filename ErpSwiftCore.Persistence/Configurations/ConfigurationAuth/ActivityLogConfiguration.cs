using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Persistence.Configurations.ConfigurationSharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationAuth
{
    public class ActivityLogConfiguration
      : BaseEntityConfiguration<ActivityLog>,
        IEntityTypeConfiguration<ActivityLog>
    {
        public override void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            base.Configure(builder);

            builder.ToTable("ActivityLogs");

            // الحقل UserId مطلوب
            builder.Property(al => al.UserId)
                   .IsRequired();

            // ActivityType مطلوب وبحد أقصى 100 حرف
            builder.Property(al => al.ActivityType)
                   .IsRequired()
                   .HasMaxLength(100);

            // IpAddress اختياري وبحد أقصى 45 حرف (IPv6)
            builder.Property(al => al.IpAddress)
                   .HasMaxLength(45);

            // DeviceInfo اختياري وبحد أقصى 255
            builder.Property(al => al.DeviceInfo)
                   .HasMaxLength(255);

            // ربط المفتاح الأجنبي (تمَّ ضبطه مسبقًا في ApplicationUserConfiguration)
        }
    }
}