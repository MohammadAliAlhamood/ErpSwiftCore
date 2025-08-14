using ErpSwiftCore.Domain.Entities.EntityAuth; 
namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface IUserProfileService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser?> GetUserProfileAsync(string userId);
        Task UpdateProfileAsync(ApplicationUser updatedUser);
        Task DeleteUserAsync(string userId);
        Task BlockUserAsync(string userId);
        Task UnblockUserAsync(string userId);
    }
}
