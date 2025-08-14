using ErpSwiftCore.Domain.Entities.EntityAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationAuth
{
    public class ApplicationUserConfiguration
       : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // اسم الجدول
            builder.ToTable("AspNetUsers"); // إذا أردت تغيير الاسم، أو اتركه كما هو

            // إنشاء فهرس على TenantId إن رغبت
            builder.HasIndex(u => u.TenantId);

            // ضبط خواص Name و ProfilePictureUrl و Address وحقل الاشتراك
            builder.Property(u => u.Name)
                   .HasMaxLength(100);

            builder.Property(u => u.ProfilePictureUrl)
                   .HasMaxLength(255);

            builder.Property(u => u.Address)
                   .HasMaxLength(500);

            builder.Property(u => u.IsSubscribedToSecurityNotifications)
                   .HasDefaultValue(false);

            // علاقة واحد إلى كثير مع ActivityLogs
            builder.HasMany(u => u.ActivityLogs)
                   .WithOne(al => al.User)
                   .HasForeignKey(al => al.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // علاقة واحد إلى كثير مع Sessions
            builder.HasMany(u => u.Sessions)
                   .WithOne(s => s.User)
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // علاقة واحد إلى كثير مع SecurityAlerts
            builder.HasMany(u => u.SecurityAlerts)
                   .WithOne(sa => sa.User)
                   .HasForeignKey(sa => sa.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // علاقة واحد إلى كثير مع TrustedDevices
            builder.HasMany(u => u.TrustedDevices)
                   .WithOne(td => td.User)
                   .HasForeignKey(td => td.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // علاقة واحد إلى كثير مع SocialAccounts
            builder.HasMany(u => u.SocialAccounts)
                   .WithOne(sa => sa.User)
                   .HasForeignKey(sa => sa.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // علاقة اختياريّة مع Company (Tenant)
            builder.HasOne(u => u.Company)
                   .WithMany()
                   .HasForeignKey(u => u.TenantId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}