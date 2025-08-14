using ErpSwiftCore.Domain.Entities.EntityAuth;

namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(
            ApplicationUser applicationUser, 
            IEnumerable<string> roles);
    }
}