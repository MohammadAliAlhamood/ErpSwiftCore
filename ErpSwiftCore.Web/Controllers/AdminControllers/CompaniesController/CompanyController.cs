using AutoMapper;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace ErpSwiftCore.Web.Controllers.CompaniesController
{
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _companyService.GetAllCompaniesAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                // إذا فشلت الاستجابة، نعرض رسالة خطأ
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                    ?? "حدث خطأ أثناء جلب قائمة الشركات.";
                // نمرر مصفوفة فارغة حتى لا يحدث NullReferenceException في الـ View
                return View(Enumerable.Empty<CompanyDto>());
            }

            // نحاول تحويل Result إلى List<CompanyDto>
            var companies = JsonConvert.DeserializeObject<List<CompanyDto>>(apiResponse.Result.ToString())
                            ?? new List<CompanyDto>();

            return View(companies);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _companyService.GetCompanyByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                    ?? "لم يتم العثور على بيانات الشركة.";
                return RedirectToAction(nameof(Index));
            }

            var model = JsonConvert.DeserializeObject<CompanyDto>(apiResponse.Result.ToString());
            if (model == null)
            {
                TempData["error"] = "تعذر فك بيانات الشركة.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            CompanyDto companyDto = new();
            if (id == null || id == Guid.Empty)
            {
                return View(companyDto);
            }

            var apiResponse = await _companyService.GetCompanyByIdAsync(id.Value);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                    ?? "لم يتم العثور على بيانات الشركة.";
                return RedirectToAction(nameof(Index));
            }

            companyDto = JsonConvert.DeserializeObject<CompanyDto>(apiResponse.Result.ToString());
            if (companyDto == null)
            {
                TempData["error"] = "تعذر فك بيانات الشركة.";
                return RedirectToAction(nameof(Index));
            }

            return View(companyDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CompanyDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Id == Guid.Empty)
            {

                var createDto = _mapper.Map<CompanyCreateDto>(model);
                var apiResponse = await _companyService.CreateCompanyAsync(createDto);

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء الشركة.");
                    return View(model);
                }

                TempData["success"] = "تم إنشاء الشركة بنجاح.";
            }
            else
            {
                // تعديل بيانات الشركة
                var updateDto = _mapper.Map<CompanyUpdateDto>(model);
                var apiResponse = await _companyService.UpdateCompanyAsync(updateDto);

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث بيانات الشركة.");
                    return View(model);
                }

                TempData["success"] = "تم تحديث بيانات الشركة بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiResponse = await _companyService.DeleteCompanyAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف الشركة.";
            }
            else
            {
                TempData["success"] = "تم حذف الشركة بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var apiResponse = await _companyService.DeleteAllCompaniesAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع الشركات.";
            }
            else
            {
                TempData["success"] = "تم حذف جميع الشركات بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
