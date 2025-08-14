using AutoMapper;
using ErpSwiftCore.Web.IService.ICRMsService;
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.CRMsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class SupplierController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISupplierService _supplierService;
         
        public SupplierController(ISupplierService supplierService, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _supplierService.GetAllAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب الموردين.";
                return View(Enumerable.Empty<SupplierDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<SupplierDto>>(apiResponse.Result?.ToString() ?? "")
                ?? new List<SupplierDto>();

            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _supplierService.GetByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على المورد.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<SupplierDto>(apiResponse.Result?.ToString() ?? "");
            if (dto == null)
            {
                TempData["error"] = "تعذّر فكّ بيانات المورد.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }


        // GET Upsert
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new SupplierDto();

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _supplierService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                       ?? "تعذّر الاتصال بخدمة الموردين.";
                    return RedirectToAction(nameof(Index));
                }

                vm = JsonConvert
                    .DeserializeObject<SupplierDto>(resp.Result?.ToString() ?? "")
                    ?? new SupplierDto();
            }

            return View(vm);
        }


        // POST Upsert
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SupplierDto vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            bool isNew = vm.ID == Guid.Empty;

            // 1. فحص التكرار على كل حقل
            if (await Exists(
                    isNew,
                    () => _supplierService.CheckExistsByCodeAsync(vm.SupplierCode),
                    () => _supplierService.CheckExistsByCodeAsync(vm.SupplierCode))) // يمكن استبدال بالدالة Excluding إن وجدت
            {
                ModelState.AddModelError(
                    nameof(vm.SupplierCode),
                    isNew
                        ? "رمز المورد مستخدم بالفعل، الرجاء تغييره."
                        : "رمز المورد مستخدم من قبل مورد آخر.");
            }

            if (await Exists(
                    isNew,
                    () => _supplierService.CheckExistsByEmailAsync(vm.ContactInfo.Email),
                    () => _supplierService.CheckExistsByEmailExcludingAsync(vm.ContactInfo.Email, vm.ID)))
            {
                ModelState.AddModelError(
                    nameof(vm.ContactInfo.Email),
                    "البريد الإلكتروني مسجّل بالفعل لمورد آخر.");
            }

            if (await Exists(
                    isNew,
                    () => _supplierService.CheckExistsByNationalIdAsync(vm.NationalID),
                    () => _supplierService.CheckExistsByNationalIdExcludingAsync(vm.NationalID, vm.ID)))
            {
                ModelState.AddModelError(
                    nameof(vm.NationalID),
                    "الرقم الوطني مسجّل بالفعل لمورد آخر.");
            }

            if (await Exists(
                    isNew,
                    () => _supplierService.CheckExistsByPhoneAsync(vm.ContactInfo.Phone),
                    () => _supplierService.CheckExistsByPhoneExcludingAsync(vm.ContactInfo.Phone, vm.ID)))
            {
                ModelState.AddModelError(
                    nameof(vm.ContactInfo.Phone),
                    "رقم الجوال مسجّل بالفعل لمورد آخر.");
            }
 
            // 2. تنفيذ الإنشاء أو التحديث
            APIResponseDto? apiResponse;
            if (isNew)
            {
                apiResponse = await _supplierService.CreateAsync(_mapper.Map<CreateSupplierDto>(vm));
                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault()
                        ?? "فشل الاتصال بخدمة الإنشاء.");
                    return View(vm);
                }

                TempData["success"] = "تم إنشاء المورد بنجاح.";
            }
            else
            {
                apiResponse = await _supplierService.UpdateAsync(_mapper.Map<UpdateSupplierDto>(vm));
                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault()
                        ?? "فشل الاتصال بخدمة التحديث.");
                    return View(vm);
                }

                TempData["success"] = "تم تحديث بيانات المورد بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiResponse = await _supplierService.DeleteAsync(id);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف المورد بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف المورد.";

            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي مورد للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var apiResponse = await _supplierService.DeleteRangeAsync(ids);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف مجموعة الموردين بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة الموردين.";

            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// يتحقّق من نجاح العملية (IsSuccess) ثم يحوّل Result إلى bool.
        /// يختار بين queryNew و queryExcluding بناءً على isNew.
        /// </summary>
        private async Task<bool> Exists(bool isNew, Func<Task<APIResponseDto?>> queryNew, Func<Task<APIResponseDto?>>? queryExcluding = null)
        {
            var resp = (isNew || queryExcluding == null)
                ? await queryNew()
                : await queryExcluding();

            if (resp == null || !resp.IsSuccess)
                return false;

            if (resp.Result == null)
                return false;

            return Convert.ToBoolean(resp.Result);
        }
    }
}
