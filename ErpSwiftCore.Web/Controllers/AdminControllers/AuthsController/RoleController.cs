using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.Role;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ErpSwiftCore.Web.Controllers.AdminControllers.AuthsController
{
    [Authorize(Roles = SD.Role_Admin)]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: /Admin/Auths/Role
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _roleService.GetAllRolesAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                var err = apiResponse?.ErrorMessages.FirstOrDefault() ?? "حدث خطأ غير متوقع.";
                return RedirectToAction("Error", "Home", new { message = err });
            }

            var roles = JsonConvert.DeserializeObject<List<RoleDto>>(apiResponse.Result.ToString())!;
            return View(roles);
        }

        // GET: /Admin/Auths/Role/AssignByEmail
        [HttpGet]
        public IActionResult AssignByEmail()
        {
            return View();
        }

        // POST: /Admin/Auths/Role/AssignByEmail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignByEmail(AssignRoleByEmailRequestDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var apiResponse = await _roleService.AssignRoleByEmailAsync(dto);
            TempData["FlashMessage"] = apiResponse?.IsSuccess == true
                ? "تم تعيين الدور للمستخدم عبر البريد الإلكتروني بنجاح."
                : apiResponse?.ErrorMessages.FirstOrDefault();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Auths/Role/Assign
        [HttpGet]
        public async Task<IActionResult> Assign()
        {
            await PopulateRolesAsync();
            return View();
        }

        // POST: /Admin/Auths/Role/Assign
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(AssignRoleRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                await PopulateRolesAsync();
                return View(dto);
            }

            var apiResponse = await _roleService.AssignRoleAsync(dto);
            TempData["FlashMessage"] = apiResponse?.IsSuccess == true
                ? "تم تعيين الدور للمستخدم بنجاح."
                : apiResponse?.ErrorMessages.FirstOrDefault();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Auths/Role/Permissions
        [HttpGet]
        public async Task<IActionResult> Permissions()
        {
            var apiResponse = await _roleService.GetAllRolesAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                var err = apiResponse?.ErrorMessages.FirstOrDefault() ?? "حدث خطأ غير متوقع.";
                return RedirectToAction("Error", "Home", new { message = err });
            }

            var roles = JsonConvert.DeserializeObject<List<RoleDto>>(apiResponse.Result.ToString())!;
            return View(roles);
        }

        // POST: /Admin/Auths/Role/UpdatePermissions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePermissions(UpdateRolePermissionsRequestDto dto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Permissions));

            var apiResponse = await _roleService.UpdateRolePermissionsAsync(dto);
            TempData["FlashMessage"] = apiResponse?.IsSuccess == true
                ? "تم تحديث صلاحيات الدور بنجاح."
                : apiResponse?.ErrorMessages.FirstOrDefault();
            return RedirectToAction(nameof(Permissions));
        }

        private async Task PopulateRolesAsync()
        {
            var response = await _roleService.GetAllRolesAsync();
            if (response?.IsSuccess == true)
            {
                var roles = JsonConvert.DeserializeObject<List<RoleDto>>(response.Result.ToString())!;
                ViewBag.Roles = roles.Select(r =>
                    new SelectListItem(r.Name, r.Name)
                ).ToList();
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
