using ErpSwiftCore.Domain.Entities.EntityFinancial;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace ErpSwiftCore.Persistence.Configurations.ConfigurationFinancial
{
    public class CustomFinancialReportResultConfiguration : IEntityTypeConfiguration<CustomFinancialReportResult>
    {
        public void Configure(EntityTypeBuilder<CustomFinancialReportResult> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();

            builder.Property(x => x.ReportName).IsRequired().HasMaxLength(200);

            // ValueConverter for List<Dictionary<string, object>>
            var dataConverter = new ValueConverter<List<Dictionary<string, object>>?, string?>(
                v => v == null ? null : JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => v == null ? null : JsonSerializer.Deserialize<List<Dictionary<string, object>>>(v, (JsonSerializerOptions?)null)
            );
            builder.Property(x => x.Data)
                   .HasConversion(dataConverter)
                   .HasColumnType("nvarchar(max)");
        }
    }
}