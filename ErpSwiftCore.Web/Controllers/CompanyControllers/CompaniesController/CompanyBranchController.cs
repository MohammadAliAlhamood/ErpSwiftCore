using AutoMapper;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompanyBranchs;
 using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.CompaniesController
{
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyBranchController : Controller
    {
        private readonly ICompanyBranchService _branchService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyBranchController(
            ICompanyBranchService branchService,
            ICompanyService companyService,
            IMapper mapper)
        {
            _branchService = branchService;
            _companyService = companyService;
            _mapper = mapper;
        }

        // GET: /CompanyBranch
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _branchService.GetAllBranchesAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                  ?? "حدث خطأ أثناء جلب قائمة الفروع.";
                return View(Enumerable.Empty<CompanyBranchDto>());
            }

            var branches = JsonConvert
                .DeserializeObject<List<CompanyBranchDto>>(apiResponse.Result.ToString())
                ?? new List<CompanyBranchDto>();

            return View(branches);
        }

        // GET: /CompanyBranch/Details/{id}
        // يستدعي GetBranchByIdAsync لعرض بيانات الفرع الأساسية
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _branchService.GetBranchByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                  ?? "لم يتم العثور على بيانات الفرع.";
                return RedirectToAction(nameof(Index));
            }

            var branch = JsonConvert
                .DeserializeObject<CompanyBranchDto>(apiResponse.Result.ToString());
            if (branch == null)
            {
                TempData["error"] = "تعذر فك بيانات الفرع.";
                return RedirectToAction(nameof(Index));
            }

            return View(branch);
        }

        // GET: /CompanyBranch/DetailsWithCompany/{id}
        // يستدعي GetBranchWithCompanyAsync لعرض بيانات الفرع مع معلومات الشركة
        [HttpGet]
        public async Task<IActionResult> DetailsWithCompany(Guid id)
        {
            var apiResponse = await _branchService.GetBranchWithCompanyAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                  ?? "لم يتم العثور على بيانات الفرع مع الشركة.";
                return RedirectToAction(nameof(Index));
            }

            var branch = JsonConvert
                .DeserializeObject<CompanyBranchDto>(apiResponse.Result.ToString());
            if (branch == null)
            {
                TempData["error"] = "تعذر فك بيانات الفرع مع الشركة.";
                return RedirectToAction(nameof(Index));
            }

            return View("Details", branch); // أو View مخصص
        }

        // GET: /CompanyBranch/ByCompany/{companyId}
        // يستدعي GetBranchesByCompanyIdAsync لجلب كل فروع الشركة
        [HttpGet]
        public async Task<IActionResult> ByCompany(Guid companyId)
        {
            var apiResponse = await _branchService.GetBranchesByCompanyIdAsync(companyId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                  ?? "فشل جلب فروع الشركة.";
                return RedirectToAction(nameof(Index));
            }

            var branches = JsonConvert
                .DeserializeObject<List<CompanyBranchDto>>(apiResponse.Result.ToString())
                ?? new List<CompanyBranchDto>();

            ViewData["Title"] = $"فروع الشركة ({companyId})";
            return View("Index", branches);
        }

        // GET: /CompanyBranch/Upsert/{id?}
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            // استعلام قائمة الشركات
            var compResp = await _companyService.GetAllCompaniesAsync();
            var companies = compResp != null && compResp.IsSuccess
                ? JsonConvert.DeserializeObject<List<CompanyDto>>(compResp.Result.ToString())
                : new List<CompanyDto>();

            var vm = new CompanyBranchViewModel
            {
                CompanyBranch = new CompanyBranchDto(),
                CompanyList = companies
                    .Select(c => new SelectListItem { Text = c.CompanyName, Value = c.Id.ToString() })
                    .ToList()
            };

            if (id == null || id == Guid.Empty)
                return View(vm);

            // جلب بيانات الفرع للتعديل
            var apiResponse = await _branchService.GetBranchByIdAsync(id.Value);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                  ?? "لم يتم العثور على بيانات الفرع.";
                return RedirectToAction(nameof(Index));
            }

            var branchDto = JsonConvert
                .DeserializeObject<CompanyBranchDto>(apiResponse.Result.ToString());
            if (branchDto == null)
            {
                TempData["error"] = "تعذر فك بيانات الفرع.";
                return RedirectToAction(nameof(Index));
            }

            vm.CompanyBranch = branchDto;
            return View(vm);
        }

        // POST: /CompanyBranch/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CompanyBranchViewModel vm)
        {
            //if (!ModelState.IsValid)
            //{
            //    // إعادة تعبئة قائمة الشركات إن كان هناك خطأ في التحقق
            //    var compResp = await _companyService.GetAllCompaniesAsync();
            //    var companies = (compResp != null && compResp.IsSuccess)
            //        ? JsonConvert.DeserializeObject<List<CompanyDto>>(compResp.Result.ToString())
            //        : new List<CompanyDto>();

            //    vm.CompanyList = companies
            //        .Select(c => new SelectListItem { Text = c.CompanyName, Value = c.Id.ToString() })
            //        .ToList();

            //    return View(vm);
            //}

            if (vm.CompanyBranch.Id == Guid.Empty)
            {
                // إنشاء
                var createDto = _mapper.Map<CompanyBranchCreateDto>(vm.CompanyBranch);
                var response = await _branchService.CreateBranchAsync(createDto);
                if (response == null || !response.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty,
                        response?.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء الفرع.");
                    return View(vm);
                }

                TempData["success"] = "تم إنشاء الفرع بنجاح.";
            }
            else
            {
                // تحديث
                var updateDto = _mapper.Map<CompanyBranchUpdateDto>(vm.CompanyBranch);
                var response = await _branchService.UpdateBranchAsync(updateDto);
                if (response == null || !response.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty,
                        response?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث بيانات الفرع.");
                    return View(vm);
                }

                TempData["success"] = "تم تحديث بيانات الفرع بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /CompanyBranch/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _branchService.DeleteBranchAsync(id);
            if (response == null || !response.IsSuccess)
                TempData["error"] = response?.ErrorMessages.FirstOrDefault() ?? "فشل حذف الفرع.";
            else
                TempData["success"] = "تم حذف الفرع بنجاح.";

            return RedirectToAction(nameof(Index));
        }

        // POST: /CompanyBranch/DeleteAll/{companyId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll(Guid companyId)
        {
            var response = await _branchService.DeleteAllBranchesAsync(companyId);
            if (response == null || !response.IsSuccess)
                TempData["error"] = response?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع فروع الشركة.";
            else
                TempData["success"] = "تم حذف جميع فروع الشركة بنجاح.";

            return RedirectToAction(nameof(Index));
        }
    }
}
