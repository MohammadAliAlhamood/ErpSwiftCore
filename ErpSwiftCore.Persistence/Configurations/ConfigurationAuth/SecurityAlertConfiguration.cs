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
    public class SecurityAlertConfiguration
           : BaseEntityConfiguration<SecurityAlert>,
             IEntityTypeConfiguration<SecurityAlert>
    {
        public override void Configure(EntityTypeBuilder<SecurityAlert> builder)
        {
            base.Configure(builder);

            builder.ToTable("SecurityAlerts");

            builder.Property(sa => sa.UserId)
                   .IsRequired(false); // قد يكون Null إذا أرسل التنبيه لجهة عامّة؟

            builder.Property(sa => sa.Message)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(sa => sa.AlertTime)
                   .IsRequired();

            // المفتاح الأجنبي UserId مُعَدّ مسبقًا
        }
    }
}