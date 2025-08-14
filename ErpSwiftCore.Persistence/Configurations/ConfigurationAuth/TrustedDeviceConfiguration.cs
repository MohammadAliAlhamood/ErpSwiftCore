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
    public class TrustedDeviceConfiguration
     : BaseEntityConfiguration<TrustedDevice>,
       IEntityTypeConfiguration<TrustedDevice>
    {
        public override void Configure(EntityTypeBuilder<TrustedDevice> builder)
        {
            base.Configure(builder);

            builder.ToTable("TrustedDevices");


            builder.Property(td => td.UserId)
                   .IsRequired();

            builder.Property(td => td.DeviceName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(td => td.AddedAt)
                   .IsRequired();
        }
    }
}