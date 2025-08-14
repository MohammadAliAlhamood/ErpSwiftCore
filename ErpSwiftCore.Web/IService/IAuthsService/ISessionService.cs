using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.ActivityLogs;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Session;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.IService.IAuthsService
{
    public interface ISessionService
    {
        Task<APIResponseDto?> EndSessionAsync(EndSessionRequestDto dto);
        Task<APIResponseDto?> GetSystemUsageStatisticsAsync();
        Task<APIResponseDto?> GetUserActivityLogsAsync(GetUserActivityLogsRequestDto dto);
        Task<APIResponseDto?> GetActiveSessionsAsync(GetActiveSessionsRequestDto dto);
    }
}
