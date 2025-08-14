using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models;
using ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.PasswordSecurity;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpSwiftCore.Web.Controllers.AdminControllers.AuthsController
{
    public class PasswordSecurityController : Controller
    {
        private readonly IPasswordSecurityService _passwordService;

        public PasswordSecurityController(IPasswordSecurityService passwordService)
        {
            _passwordService = passwordService;
        }

        // ============================================
        // CHANGE PASSWORD
        // ============================================

        [HttpGet("password/change")]
        [Authorize]  // أي مستخدم مسجل يمكنه تغيير كلمة المرور
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordRequestDto());
        }

        [HttpPost("password/change")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var apiResponse = await _passwordService.ChangePasswordAsync(dto);
            if (apiResponse == null)
            {
                ModelState.AddModelError(string.Empty, "خطأ غير متوقع أثناء تغيير كلمة المرور.");
                return View(dto);
            }

            if (!apiResponse.IsSuccess)
            {
                foreach (var err in apiResponse.ErrorMessages)
                    ModelState.AddModelError(string.Empty, err);
                return View(dto);
            }

            TempData["SuccessMessage"] = "تم تغيير كلمة المرور بنجاح.";
            return RedirectToAction("Index", "Home");
        }


        // ============================================
        // FORGOT PASSWORD
        // ============================================

        [HttpGet("password/forgot")]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordRequestDto());
        }

        [HttpPost("password/forgot")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var apiResponse = await _passwordService.ForgotPasswordAsync(dto);
            if (apiResponse == null)
            {
                ModelState.AddModelError(string.Empty, "خطأ غير متوقع أثناء طلب إعادة تعيين كلمة المرور.");
                return View(dto);
            }

            if (!apiResponse.IsSuccess)
            {
                foreach (var err in apiResponse.ErrorMessages)
                    ModelState.AddModelError(string.Empty, err);
                return View(dto);
            }

            TempData["SuccessMessage"] = "تم إرسال رابط إعادة التعيين إلى بريدك الإلكتروني.";
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet("password/forgot-confirmation")]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // ============================================
        // RESET PASSWORD
        // ============================================

        [HttpGet("password/reset")]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId, string resetToken)
        {
            var model = new ResetPasswordRequestDto
            {
                UserId = userId,
                ResetToken = resetToken
            };
            return View(model);
        }


        [HttpPost("password/reset")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var apiResponse = await _passwordService.ResetPasswordAsync(dto);
            if (apiResponse == null)
            {
                ModelState.AddModelError(string.Empty, "خطأ غير متوقع أثناء إعادة تعيين كلمة المرور.");
                return View(dto);
            }

            if (!apiResponse.IsSuccess)
            {
                foreach (var err in apiResponse.ErrorMessages)
                    ModelState.AddModelError(string.Empty, err);
                return View(dto);
            }

            TempData["SuccessMessage"] = "تم إعادة تعيين كلمة المرور بنجاح.";
            return RedirectToAction("Index", "Home");
        }
    }
}
