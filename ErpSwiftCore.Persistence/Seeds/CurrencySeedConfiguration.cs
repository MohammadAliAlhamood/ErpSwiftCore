using ErpSwiftCore.SharedKernel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ErpSwiftCore.Persistence.Seeds
{
    public class CurrencySeedConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasData(
             new Currency
     {
         ID = Guid.Parse("11111111-1111-1111-1111-111111111111"),
         CurrencyCode = "USD",
         CurrencyName = "US Dollar"
     },
             new Currency
             {
                 ID = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                 CurrencyCode = "EUR",
                 CurrencyName = "Euro"
             },
             new Currency
             {
                 ID = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                 CurrencyCode = "JPY",
                 CurrencyName = "Japanese Yen"
             },
             new Currency
             {
                 ID = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                 CurrencyCode = "GBP",
                 CurrencyName = "British Pound"
             },
             new Currency
             {
                 ID = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                 CurrencyCode = "AUD",
                 CurrencyName = "Australian Dollar"
             },
             new Currency
             {
                 ID = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                 CurrencyCode = "CAD",
                 CurrencyName = "Canadian Dollar"
             },
             new Currency
             {
                 ID = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                 CurrencyCode = "CHF",
                 CurrencyName = "Swiss Franc"
             },
             new Currency
             {
                 ID = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                 CurrencyCode = "CNY",
                 CurrencyName = "Chinese Yuan"
             },
             new Currency
             {
                 ID = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                 CurrencyCode = "SEK",
                 CurrencyName = "Swedish Krona"
             },
             new Currency
             {
                 ID = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                 CurrencyCode = "NZD",
                 CurrencyName = "New Zealand Dollar"
             },
             new Currency
             {
                 ID = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                 CurrencyCode = "MXN",
                 CurrencyName = "Mexican Peso"
             },
             new Currency
             {
                 ID = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                 CurrencyCode = "SGD",
                 CurrencyName = "Singapore Dollar"
             },
             new Currency
             {
                 ID = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                 CurrencyCode = "HKD",
                 CurrencyName = "Hong Kong Dollar"
             },
             new Currency
             {
                 ID = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                 CurrencyCode = "NOK",
                 CurrencyName = "Norwegian Krone"
             },
             new Currency
             {
                 ID = Guid.Parse("f1f1f1f1-f1f1-f1f1-f1f1-f1f1f1f1f1f1"),
                 CurrencyCode = "KRW",
                 CurrencyName = "South Korean Won"
             },
             new Currency
             {
                 ID = Guid.Parse("f2f2f2f2-f2f2-f2f2-f2f2-f2f2f2f2f2f2"),
                 CurrencyCode = "TRY",
                 CurrencyName = "Turkish Lira"
             },
             new Currency
             {
                 ID = Guid.Parse("f3f3f3f3-f3f3-f3f3-f3f3-f3f3f3f3f3f3"),
                 CurrencyCode = "RUB",
                 CurrencyName = "Russian Ruble"
             },
             new Currency
             {
                 ID = Guid.Parse("f4f4f4f4-f4f4-f4f4-f4f4-f4f4f4f4f4f4"),
                 CurrencyCode = "INR",
                 CurrencyName = "Indian Rupee"
             },
             new Currency
             {
                 ID = Guid.Parse("f5f5f5f5-f5f5-f5f5-f5f5-f5f5f5f5f5f5"),
                 CurrencyCode = "BRL",
                 CurrencyName = "Brazilian Real"
             },
             new Currency
             {
                 ID = Guid.Parse("f6f6f6f6-f6f6-f6f6-f6f6-f6f6f6f6f6f6"),
                 CurrencyCode = "ZAR",
                 CurrencyName = "South African Rand"
             },
             new Currency
             {
                 ID = Guid.Parse("f7f7f7f7-f7f7-f7f7-f7f7-f7f7f7f7f7f7"),
                 CurrencyCode = "DKK",
                 CurrencyName = "Danish Krone"
             },
             new Currency
             {
                 ID = Guid.Parse("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"),
                 CurrencyCode = "PLN",
                 CurrencyName = "Polish Zloty"
             },
             new Currency
             {
                 ID = Guid.Parse("f9f9f9f9-f9f9-f9f9-f9f9-f9f9f9f9f9f9"),
                 CurrencyCode = "THB",
                 CurrencyName = "Thai Baht"
             },
             new Currency
             {
                 ID = Guid.Parse("fafafafa-fafa-fafa-fafa-fafafafafafa"),
                 CurrencyCode = "IDR",
                 CurrencyName = "Indonesian Rupiah"
             },
             new Currency
             {
                 ID = Guid.Parse("fbfbfbfb-fbfb-fbfb-fbfb-fbfbfbfbfbfb"),
                 CurrencyCode = "MYR",
                 CurrencyName = "Malaysian Ringgit"
             }
         );
         
         


        }
    }
}