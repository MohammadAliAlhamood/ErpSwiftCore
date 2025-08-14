using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.ActivityLogs;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Session;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.SystemStatistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Controllers.AdminControllers.AuthsController
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// عرض جميع الجلسات النشطة حالياً
        /// </summary>
        /// <summary>
        /// عرض الجلسات النشطة بناءً على المعايير المرسلة
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ActiveSessions([FromQuery] GetActiveSessionsRequestDto dto)
        {
            var apiResponse = await _sessionService.GetActiveSessionsAsync(dto);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                ModelState.AddModelError(string.Empty,
                    apiResponse?.ErrorMessages.FirstOrDefault() ?? "خطأ في جلب الجلسات النشطة.");
                return View(Enumerable.Empty<GetActiveSessionsRequestDto>());
            }

            // نتوقع أن تكون النتيجة قائمة من ActiveSessionDto
            var sessions = JsonConvert
                .DeserializeObject<List<GetActiveSessionsRequestDto>>(apiResponse.Result.ToString())!;
            return View(sessions);
        }

        /// <summary>
        /// إنهاء جلسة معينة
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EndSession(EndSessionRequestDto dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(ActiveSessions));

            var apiResponse = await _sessionService.EndSessionAsync(dto);
            if (apiResponse == null || !apiResponse.IsSuccess)
                TempData["Error"] = apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل إنهاء الجلسة.";
            else
                TempData["Success"] = "تم إنهاء الجلسة بنجاح.";

            return RedirectToAction(nameof(ActiveSessions));
        }

        /// <summary>
        /// عرض سجل نشاط المستخدم الحالي
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ActivityLogs()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var dto = new GetUserActivityLogsRequestDto { UserId = userId! };

            var apiResponse = await _sessionService.GetUserActivityLogsAsync(dto);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                ModelState.AddModelError("", apiResponse?.ErrorMessages.FirstOrDefault() ?? "خطأ في جلب سجل النشاط.");
                return View(Enumerable.Empty<ActivityLogDto>());
            }

            var json = apiResponse.Result.ToString();
            var logs = JsonConvert.DeserializeObject<List<ActivityLogDto>>(json)!;
            return View(logs);
        }

        /// <summary>
        /// عرض إحصائيات استخدام النظام
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> SystemUsage()
        {
            var apiResponse = await _sessionService.GetSystemUsageStatisticsAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                ModelState.AddModelError("", apiResponse?.ErrorMessages.FirstOrDefault() ?? "خطأ في جلب إحصائيات النظام.");
                return View();
            }

            var json = apiResponse.Result.ToString();
            var stats = JsonConvert.DeserializeObject<SystemUsageStatisticsDto>(json)!;
            return View(stats);
        }
    }
}
