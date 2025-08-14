using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Authentication;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Role;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserManagement;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserProfile;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
namespace ErpSwiftCore.Web.Controllers.AdminControllers.AuthsController
{
    public class UserProfileController : Controller
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserProfileService _userProfileService;
        private readonly IRoleService _roleService;
        private readonly IAuthenticationLocService _authService;

        public UserProfileController(ITokenProvider tokenProvider, IUserProfileService userProfileService, IRoleService roleService, IAuthenticationLocService authService)
        {
            _tokenProvider = tokenProvider;
            _userProfileService = userProfileService;
            _roleService = roleService;
            _authService = authService;
        }
        [HttpGet("profile/{userId?}")]
        [Authorize]
        public async Task<IActionResult> Profile(string? userId)
        {
            APIResponseDto? apiResponse;

            if (string.IsNullOrEmpty(userId))
            {
                // جلب بروفايل المستخدم الحالي
                apiResponse = await _userProfileService.GetMyProfileAsync();
            }
            else
            {
                // للتأكد من صلاحيات مشاهدة بروفايل آخر
                if (!User.IsInRole(SD.Role_Admin)
                    && !User.IsInRole(SD.Role_HRManager)
                    && !User.IsInRole(SD.Role_SeniorManager))
                {
                    return Forbid();
                }

                apiResponse = await _userProfileService.GetUserProfileAsync(userId);
            }
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                var err = apiResponse?.ErrorMessages.FirstOrDefault() ?? "حدث خطأ غير متوقع.";
                return RedirectToAction("Error", "Home", new { message = err });
            }
            var json = apiResponse.Result.ToString();
            var model = JsonConvert.DeserializeObject<UserDto>(json)!;
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HRManager + "," + SD.Role_SeniorManager)]
        public async Task<IActionResult> Users()
        {
            var apiResponse = await _userProfileService.GetAllUsersAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
                return RedirectToAction("Error", "Home", new { message = apiResponse?.ErrorMessages.FirstOrDefault() });

            var json = apiResponse.Result.ToString();
            var list = JsonConvert.DeserializeObject<List<UserDto>>(json);
            return View(list);
        } 
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HRManager + "," + SD.Role_ManagementEmployee + "," + SD.Role_RegularEmployee)]
        public async Task<IActionResult> UpdateProfile(UpdateProfileRequestDto dto)
        {
            if (!ModelState.IsValid)
                return View(nameof(Profile), dto);

            var apiResponse = await _userProfileService.UpdateProfileAsync(dto);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["ErrorMessage"] = apiResponse?.ErrorMessages.FirstOrDefault();
                return RedirectToAction(nameof(Profile), new { userId = dto.Id });
            }

            TempData["SuccessMessage"] = "Profile updated successfully.";
            return RedirectToAction(nameof(Profile), new { userId = dto.Id });
        }
        
        
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HRManager)]
        public async Task<IActionResult> BlockUser(BlockUserRequestDto dto)
        {
            var apiResponse = await _userProfileService.BlockUserAsync(dto);
            TempData["FlashMessage"] = apiResponse?.IsSuccess == true
                ? "User blocked successfully."
                : apiResponse?.ErrorMessages.FirstOrDefault();
            return RedirectToAction("Users");
        }
        
        
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> DeleteUser(DeleteUserRequestDto dto)
        {
            var apiResponse = await _userProfileService.DeleteUserAsync(dto);
            TempData["FlashMessage"] = apiResponse?.IsSuccess == true
                ? "User deleted successfully."
                : apiResponse?.ErrorMessages.FirstOrDefault();
            return RedirectToAction("Users");
        }
        
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateMyProfile(UpdateMyProfileRequestDto dto)
        {
            if (!ModelState.IsValid)
                return View(nameof(Profile), dto);

            var apiResponse = await _userProfileService.UpdateMyProfileAsync(dto);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["ErrorMessage"] = apiResponse?.ErrorMessages.FirstOrDefault();
                return RedirectToAction(nameof(Profile));
            }

            TempData["SuccessMessage"] = "تم تحديث ملفك الشخصي بنجاح.";
            return RedirectToAction(nameof(Profile));
        }
       
        
        [HttpPost]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_HRManager)]
        public async Task<IActionResult> UnblockUser(BlockUserRequestDto dto)
        {
            var apiResponse = await _userProfileService.UnblockUserAsync(dto);
            TempData["FlashMessage"] = apiResponse?.IsSuccess == true
                ? "User unblocked successfully."
                : apiResponse?.ErrorMessages.FirstOrDefault();
            return RedirectToAction("Users");
        }






        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            await PopulateRolesAsync();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            await PopulateRolesAsync();

            if (!ModelState.IsValid)
                return View(dto);

            var registerResponse = await _authService.RegisterAsync(dto);
            if (registerResponse is null || !registerResponse.IsSuccess)
            {
                ModelState.AddModelError(string.Empty,
                    registerResponse?.ErrorMessages.FirstOrDefault() ?? "فشل التسجيل.");
                return View(dto);
            }

            if (string.IsNullOrWhiteSpace(dto.RoleName))
                dto.RoleName = SD.Role_Company;
            var assignDto = new AssignRoleByEmailRequestDto
            {
                Email = dto.Email,
                RoleName = dto.RoleName
            };
            var assignResponse = await _roleService.AssignRoleByEmailAsync(assignDto);
            if (assignResponse is null || !assignResponse.IsSuccess)
            {
                ModelState.AddModelError(string.Empty,
                    assignResponse?.ErrorMessages.FirstOrDefault()
                    ?? $"تعذر تعيين الدور '{dto.RoleName}'.");
                return View(dto);
            }

            TempData["SuccessMessage"] = "تم التسجيل بنجاح. يمكنك تسجيل الدخول الآن.";
            return RedirectToAction(nameof(Users));
        }

        private async Task PopulateRolesAsync()
        {
            var rolesResponse = await _roleService.GetAllRolesAsync();
            if (rolesResponse?.IsSuccess == true && rolesResponse.Result is IEnumerable<RoleDto> roles)
            {
                ViewBag.Roles = roles
                    .Select(r => new SelectListItem(r.Name, r.Name))
                    .ToList();
            }
            else
            {
                ViewBag.Roles = new List<SelectListItem>
                {
                    new("Admin", SD.Role_Admin),
                    new("Company", SD.Role_Company),
                    new("Accounting Employee", SD.Role_AccountingEmployee),
                    new("Management Employee", SD.Role_ManagementEmployee),
                    new("Regular Employee", SD.Role_RegularEmployee),
                    new("Workshop Manager", SD.Role_WorkshopManager),
                    new("Senior Manager", SD.Role_SeniorManager),
                    new("HR Manager", SD.Role_HRManager)
                };
            }
        }

    }
}
