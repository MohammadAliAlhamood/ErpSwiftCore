using ErpSwiftCore.Domain.Entities.EntityAuth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IAuthsService
{
    public interface ISessionService
    {

        Task<IEnumerable<Session>> GetActiveSessionsAsync(string userId);
        Task EndSessionAsync(string sessionId);
        Task AddTrustedDeviceAsync(TrustedDevice device);
        Task RemoveTrustedDeviceAsync(string deviceId);
        Task<IEnumerable<TrustedDevice>> GetTrustedDevicesAsync(string userId);

    }
}
