using ErpSwiftCore.Domain.Entities.EntityAuth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface ISocialSSOService
    { 

      Task<string> GenerateSSOUrlAsync(string provider, string callbackUrl);
      Task<(ApplicationUser User, string Token)?> HandleSSOCallbackAsync(string code, string state);
      Task LinkSocialAccountAsync(SocialAccount socialAccount);
      Task UnlinkSocialAccountAsync(string userId, string provider);
    }
}
