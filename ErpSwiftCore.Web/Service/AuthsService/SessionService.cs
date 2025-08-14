using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.ActivityLogs;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Session;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;

namespace ErpSwiftCore.Web.Service.AuthsService
{
    public class SessionService : ISessionService
    {

        private readonly IBaseService _baseService;

        public SessionService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<APIResponseDto?> EndSessionAsync(EndSessionRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/session/end"
            });
        }

        public async Task<APIResponseDto?> GetSystemUsageStatisticsAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/session/stats"
            });
        }

        public async Task<APIResponseDto?> GetUserActivityLogsAsync(GetUserActivityLogsRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/session/activity-logs"
            });
        }

        public async Task<APIResponseDto?> GetActiveSessionsAsync(GetActiveSessionsRequestDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto()
            {
                ApiType = SD.ApiType.GET,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/session/active"
            });
        }


    }
}
