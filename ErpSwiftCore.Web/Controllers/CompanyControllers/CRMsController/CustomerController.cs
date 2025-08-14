using AutoMapper;
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.IService.ICRMsService;
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.CustomerModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.CRMsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _customerService.GetAllAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب العملاء.";
                return View(Enumerable.Empty<CustomerDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<CustomerDto>>(apiResponse.Result.ToString())
                ?? new List<CustomerDto>();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _customerService.GetByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على العميل.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert.DeserializeObject<CustomerDto>(apiResponse.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات العميل.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

















        // GET Upsert
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new CustomerDto();

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _customerService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                       ?? "تعذّر الاتصال بخدمة العملاء.";
                    return RedirectToAction(nameof(Index));
                }

                vm = JsonConvert
                    .DeserializeObject<CustomerDto>(resp.Result?.ToString() ?? "")
                    ?? new CustomerDto();
            }

            return View(vm);
        }

        // POST Upsert
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CustomerDto vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            bool isNew = vm.ID == Guid.Empty;

            // --- 1. فحص التكرار ---
            if (await Exists(
                    isNew,
                    () => _customerService.CheckExistsByCodeAsync(vm.CustomerCode),
                    () => _customerService.CheckExistsByCodeAsync(vm.CustomerCode)))  // هنا يمكنك استخدام Excluding إذا أردت
            {
                ModelState.AddModelError(
                    nameof(vm.CustomerCode),
                    isNew
                        ? "رمز العميل مستخدم بالفعل، الرجاء تغييره."
                        : "رمز العميل مستخدم من قبل عميل آخر.");
            }

            if (await Exists(
                    isNew,
                    () => _customerService.CheckExistsByEmailAsync(vm.ContactInfo.Email),
                    () => _customerService.CheckExistsByEmailExcludingAsync(vm.ContactInfo.Email, vm.ID)))
            {
                ModelState.AddModelError(nameof(vm.ContactInfo.Email), "البريد الإلكتروني مسجّل بالفعل لعميل آخر.");
            }

            if (await Exists(
                    isNew,
                    () => _customerService.CheckExistsByNationalIdAsync(vm.NationalID),
                    () => _customerService.CheckExistsByNationalIdExcludingAsync(vm.NationalID, vm.ID)))
            {
                ModelState.AddModelError(nameof(vm.NationalID), "الرقم الوطني مسجّل بالفعل لعميل آخر.");
            }

            if (await Exists(
                    isNew,
                    () => _customerService.CheckExistsByPhoneAsync(vm.ContactInfo.Phone),
                    () => _customerService.CheckExistsByPhoneExcludingAsync(vm.ContactInfo.Phone, vm.ID)))
            {
                ModelState.AddModelError(nameof(vm.ContactInfo.Phone), "رقم الجوال مسجّل بالفعل لعميل آخر.");
            }


            // --- 2. تنفيذ الإنشاء أو التحديث ---
            APIResponseDto? apiResponse;
            if (isNew)
            {
                apiResponse = await _customerService.CreateAsync(_mapper.Map<CreateCustomerDto>(vm));
                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل الاتصال بخدمة الإنشاء.");
                    return View(vm);
                }
                TempData["success"] = "تم إنشاء العميل بنجاح.";
            }
            else
            {
                apiResponse = await _customerService.UpdateAsync(_mapper.Map<UpdateCustomerDto>(vm));
                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل الاتصال بخدمة التحديث.");
                    return View(vm);
                }
                TempData["success"] = "تم تحديث بيانات العميل بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// يتحقق من نجاح العملية (IsSuccess) ثم يفكّ Result إلى bool.
        /// يختار بين queryNew و queryExcluding بناءً على isNew.
        /// </summary>
        private async Task<bool> Exists(bool isNew, Func<Task<APIResponseDto?>> queryNew, Func<Task<APIResponseDto?>>? queryExcluding = null)
        {
            var resp = (isNew || queryExcluding == null)
                ? await queryNew()
                : await queryExcluding();

            // 1) تأكد أن العملية نفسها نجحت
            if (resp == null || !resp.IsSuccess)
                return false;

            // 2) فك Result وتحويله إلى bool
            if (resp.Result == null)
                return false;

            return Convert.ToBoolean(resp.Result);
        }
















        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiResponse = await _customerService.DeleteAsync(id);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف العميل بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف العميل.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي عميل للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var apiResponse = await _customerService.DeleteRangeAsync(ids);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف مجموعة العملاء بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة العملاء.";

            return RedirectToAction(nameof(Index));
        }
    }
}
