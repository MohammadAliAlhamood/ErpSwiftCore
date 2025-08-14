using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace ErpSwiftCore.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
        {
            IConfigurationSection settingsSection = 
                builder.Configuration.GetSection("BearerApiSettings");
            string secret = settingsSection.GetValue<string>("Secret") ?? throw new InvalidOperationException("BearerApiSettings:Secret is missing in configuration.");
            string issuer = settingsSection.GetValue<string>("Issuer") ?? throw new InvalidOperationException("BearerApiSettings:Issuer is missing in configuration.");
            string audience = settingsSection.GetValue<string>("Audience") ?? throw new InvalidOperationException("BearerApiSettings:Audience is missing in configuration.");
            byte[] key = Encoding.ASCII.GetBytes(secret);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });

            return builder;
        }
    }
}

