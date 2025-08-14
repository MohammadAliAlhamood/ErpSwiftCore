using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface IRoleService
    {
 
        Task AssignRoleAsync(string userId, string roleName);

        Task AssignRoleByEmailAsync(string email, string roleName);
        Task CreateRoleAsync(string roleName);

        Task DeleteRoleAsync(string roleName);

        Task<IEnumerable<string>> GetAllRolesAsync();

        Task<IEnumerable<string>> GetUserRolesAsync(string userId);


    }
}
