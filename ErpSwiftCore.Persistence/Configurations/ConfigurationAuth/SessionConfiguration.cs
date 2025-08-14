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
    public class SessionConfiguration
      : BaseEntityConfiguration<Session>,
        IEntityTypeConfiguration<Session>
    {
        public override void Configure(EntityTypeBuilder<Session> builder)
        {
            base.Configure(builder);

            builder.ToTable("Sessions");



            builder.Property(s => s.UserId)
                   .IsRequired();




            builder.Property(s => s.IpAddress)
                   .HasMaxLength(45);
        }
    }
}