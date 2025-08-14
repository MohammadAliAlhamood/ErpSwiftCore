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
    public class SocialAccountConfiguration
         : BaseEntityConfiguration<SocialAccount>,
           IEntityTypeConfiguration<SocialAccount>
    {
        public override void Configure(EntityTypeBuilder<SocialAccount> builder)
        {
            base.Configure(builder);

            builder.ToTable("SocialAccounts");

            builder.Property(sa => sa.UserId)
                   .IsRequired();

            builder.Property(sa => sa.Provider)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(sa => sa.ProviderUserId)
                   .IsRequired()
                   .HasMaxLength(200);
        }
    }
}