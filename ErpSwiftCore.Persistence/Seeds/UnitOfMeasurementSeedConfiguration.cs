using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ErpSwiftCore.Persistence.Seeds
{
    public class UnitOfMeasurementSeedConfiguration : IEntityTypeConfiguration<UnitOfMeasurement>
    {
        public void Configure(EntityTypeBuilder<UnitOfMeasurement> builder)
        {
            builder.HasData(
                new UnitOfMeasurement { ID = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Kilogram", Abbreviation = "kg", Description = "Weight unit" },
                new UnitOfMeasurement { ID = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Meter", Abbreviation = "m", Description = "Length unit" },
                new UnitOfMeasurement { ID = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Gram", Abbreviation = "g", Description = "Weight unit" },
                new UnitOfMeasurement { ID = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Milligram", Abbreviation = "mg", Description = "Weight unit" },
                new UnitOfMeasurement { ID = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Ton", Abbreviation = "t", Description = "Weight unit (1000 kg)" },
                new UnitOfMeasurement { ID = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Millimeter", Abbreviation = "mm", Description = "Length unit" },
                new UnitOfMeasurement { ID = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Centimeter", Abbreviation = "cm", Description = "Length unit" },
                new UnitOfMeasurement { ID = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Kilometer", Abbreviation = "km", Description = "Length unit" },
                new UnitOfMeasurement { ID = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Inch", Abbreviation = "in", Description = "Length unit" },
                new UnitOfMeasurement { ID = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Foot", Abbreviation = "ft", Description = "Length unit" },
                new UnitOfMeasurement { ID = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "Yard", Abbreviation = "yd", Description = "Length unit" },
                new UnitOfMeasurement { ID = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "Mile", Abbreviation = "mi", Description = "Length unit" },
                new UnitOfMeasurement { ID = Guid.Parse("f1f1f1f1-f1f1-f1f1-f1f1-f1f1f1f1f1f1"), Name = "Liter", Abbreviation = "L", Description = "Volume unit" },
                new UnitOfMeasurement { ID = Guid.Parse("f2f2f2f2-f2f2-f2f2-f2f2-f2f2f2f2f2f2"), Name = "Milliliter", Abbreviation = "mL", Description = "Volume unit" },
                new UnitOfMeasurement { ID = Guid.Parse("f3f3f3f3-f3f3-f3f3-f3f3-f3f3f3f3f3f3"), Name = "Cubic Meter", Abbreviation = "m³", Description = "Volume unit" },
                new UnitOfMeasurement { ID = Guid.Parse("f4f4f4f4-f4f4-f4f4-f4f4-f4f4f4f4f4f4"), Name = "Cubic Centimeter", Abbreviation = "cm³", Description = "Volume unit" },
                new UnitOfMeasurement { ID = Guid.Parse("f5f5f5f5-f5f5-f5f5-f5f5-f5f5f5f5f5f5"), Name = "Square Meter", Abbreviation = "m²", Description = "Area unit" },
                new UnitOfMeasurement { ID = Guid.Parse("f6f6f6f6-f6f6-f6f6-f6f6-f6f6f6f6f6f6"), Name = "Hectare", Abbreviation = "ha", Description = "Area unit (10,000 m²)" },
                new UnitOfMeasurement { ID = Guid.Parse("f7f7f7f7-f7f7-f7f7-f7f7-f7f7f7f7f7f7"), Name = "Second", Abbreviation = "s", Description = "Time unit" },
                new UnitOfMeasurement { ID = Guid.Parse("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"), Name = "Minute", Abbreviation = "min", Description = "Time unit (60 seconds)" },
                new UnitOfMeasurement { ID = Guid.Parse("f9f9f9f9-f9f9-f9f9-f9f9-f9f9f9f9f9f9"), Name = "Hour", Abbreviation = "h", Description = "Time unit (60 minutes)" },
                new UnitOfMeasurement { ID = Guid.Parse("fafafafa-fafa-fafa-fafa-fafafafafafa"), Name = "Day", Abbreviation = "d", Description = "Time unit (24 hours)" },
                new UnitOfMeasurement { ID = Guid.Parse("fbfbfbfb-fbfb-fbfb-fbfb-fbfbfbfbfbfb"), Name = "Piece", Abbreviation = "pc", Description = "Count unit" },
                new UnitOfMeasurement { ID = Guid.Parse("fcfcfcfc-fcfc-fcfc-fcfc-fcfcfcfcfcfc"), Name = "Dozen", Abbreviation = "dz", Description = "Count unit (12 pieces)" }
            );
        }
    }
}