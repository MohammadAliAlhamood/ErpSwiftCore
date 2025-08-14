using ErpSwiftCore.Domain.Entities.EntityAuth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface IAuthenticationLocService
    {
      Task<string> RegisterAsync(ApplicationUser user, string password, string? roleName = null);
      Task<(ApplicationUser User, string Token)?> LoginAsync(string userName, string password);
      Task LogOutAsync(string userId);
      Task LogOutFromAllSessionsAsync(string userId);

    }
}
