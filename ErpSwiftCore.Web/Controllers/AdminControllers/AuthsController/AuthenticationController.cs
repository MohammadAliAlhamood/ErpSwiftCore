using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Authentication;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Role;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ErpSwiftCore.Web.Controllers.AdminControllers.AuthsController
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationLocService _authService;
        private readonly ITokenProvider _tokenProvider;
        private readonly IRoleService _roleService;
        public AuthenticationController(ITokenProvider tokenProvider, IAuthenticationLocService authService, IRoleService roleService)
        {
            _tokenProvider = tokenProvider;
            _authService = authService;
            _roleService = roleService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var apiResponse = await _authService.LoginAsync(dto);
            if (apiResponse is null || !apiResponse.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تسجيل الدخول.");
                return View(dto);
            }

            var loginJson = apiResponse.Result.ToString();
            var loginResult = JsonConvert.DeserializeObject<LoginResponseDto>(loginJson)!;

            await SignInUserAsync(loginResult);
            _tokenProvider.SetToken(loginResult.Token);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // 1) طلب تسجيل الخروج من الـ API
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                await _authService.LogoutAsync(new LogoutRequestDto { UserId = userId });
                _tokenProvider.ClearToken();
            }

            // 2) إنهاء جلسة الكوكيز
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAllSessions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                await _authService.LogoutAllSessionsAsync(new LogoutRequestDto { UserId = userId });
                _tokenProvider.ClearToken();
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }







        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        { 
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
 
            if (!ModelState.IsValid)
                return View(dto);
            var registerResponse = await _authService.RegisterAsync(dto);
            if (registerResponse is null || !registerResponse.IsSuccess)
            {
                ModelState.AddModelError(string.Empty,
                    registerResponse?.ErrorMessages.FirstOrDefault() ?? "فشل التسجيل.");
                return View(dto);
            }
            TempData["SuccessMessage"] = "تم التسجيل بنجاح. يمكنك تسجيل الدخول الآن.";
            return RedirectToAction(nameof(Login));
        }



       





        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers
        private async Task SignInUserAsync(LoginResponseDto model)
        {
            var jwt = new JwtSecurityTokenHandler()
                .ReadJwtToken(model.Token);

            var claims = jwt.Claims
                .Where(c => c.Type switch
                {
                    JwtRegisteredClaimNames.Sub => true,
                    JwtRegisteredClaimNames.Email => true,
                    JwtRegisteredClaimNames.Name => true,
                    "role" => true,
                    _ => false
                })
                .Select(c =>
                    c.Type == "role"
                        ? new Claim(ClaimTypes.Role, c.Value)
                        : new Claim(
                            c.Type == JwtRegisteredClaimNames.Sub
                                ? ClaimTypes.NameIdentifier
                                : c.Type,
                            c.Value))
                .ToList();

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }

        #endregion
    }
}
