using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ErpSwiftCore.Persistence.Services.AuthsService
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            SecurityToken? token = null;
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            List<Claim> claimList = new()
            {
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName)
            };
            if (applicationUser.TenantId.HasValue)
            {
                claimList.Add(new Claim("TenantId", applicationUser.TenantId.Value.ToString()));
            }


            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            try
            {
                token = tokenHandler.CreateToken(tokenDescriptor);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



            return tokenHandler.WriteToken(token);
        }
    }
}