using AutoMapper;
using ErpSwiftCore.Web.IService.IFinancialsService;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.CostCenterModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
namespace ErpSwiftCore.Web.Controllers.CompanyControllers.FinancialsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class CostCenterController : Controller
    {
        private readonly ICostCenterService _svc;
        private readonly IMapper _mapper;

        public CostCenterController(ICostCenterService svc, IMapper mapper)
        {
            _svc = svc ?? throw new ArgumentNullException(nameof(svc));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _svc.GetAllAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب مراكز التكلفة.";
                return View(Enumerable.Empty<CostCenterDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<CostCenterDto>>(resp.Result.ToString())
                ?? new List<CostCenterDto>();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var resp = await _svc.GetByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على مركز التكلفة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<CostCenterDto>(resp.Result.ToString())
                ?? new CostCenterDto();

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            CostCenterDto CostCenter = new CostCenterDto();


            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _svc.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "مركز التكلفة غير موجود.";
                    return RedirectToAction(nameof(Index));
                }
                CostCenter = JsonConvert.DeserializeObject<CostCenterDto>(resp.Result.ToString())!;
            }


            return View(CostCenter);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CostCenterDto CostCenter)
        {


            // validation
            if (string.IsNullOrWhiteSpace(CostCenter.CenterName))
                ModelState.AddModelError(nameof(CostCenter.CenterName), "الرجاء إدخال اسم المركز.");




            APIResponseDto? resp;
            if (CostCenter.ID == Guid.Empty)
            {
                var createDto = _mapper.Map<CreateCostCenterDto>(CostCenter);
                resp = await _svc.CreateAsync(createDto);
                TempData[resp?.IsSuccess == true ? "success" : "error"] =
                    resp?.IsSuccess == true
                        ? "تم إنشاء مركز التكلفة بنجاح."
                        : resp?.ErrorMessages.FirstOrDefault()!;
            }
            else
            {
                var updateDto = _mapper.Map<UpdateCostCenterDto>(CostCenter);
                resp = await _svc.UpdateAsync(updateDto);
                TempData[resp?.IsSuccess == true ? "success" : "error"] =
                    resp?.IsSuccess == true ? "تم تحديث مركز التكلفة بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _svc.DeleteAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف المركز بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد مراكز للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _svc.DeleteRangeAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف المراكز المحددة بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var resp = await _svc.DeleteAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف جميع المراكز بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            return RedirectToAction(nameof(Index));
        }

        // ─────────────── JSON Endpoints ───────────────
        [HttpGet]
        public async Task<IActionResult> ByParent(Guid parentId)
        {
            var resp = await _svc.GetByParentAsync(parentId);
            if (resp == null || !resp.IsSuccess)
                return BadRequest(resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب المراكز الفرعية.");

            var list = JsonConvert
                .DeserializeObject<IEnumerable<CostCenterDto>>(resp.Result.ToString())
                ?? Enumerable.Empty<CostCenterDto>();

            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> WithParent(Guid id)
        {
            var resp = await _svc.GetWithParentAsync(id);
            if (resp == null || !resp.IsSuccess)
                return BadRequest(resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب المركز مع الأصل.");

            var dto = JsonConvert
                .DeserializeObject<CostCenterDto>(resp.Result.ToString())
                ?? new CostCenterDto();

            return Json(dto);
        }

        [HttpGet]
        public async Task<IActionResult> WithChildren(Guid id)
        {
            var resp = await _svc.GetWithChildrenAsync(id);
            if (resp == null || !resp.IsSuccess)
                return BadRequest(resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب المركز مع التفرعات.");

            var list = JsonConvert
                .DeserializeObject<IEnumerable<CostCenterDto>>(resp.Result.ToString())
                ?? Enumerable.Empty<CostCenterDto>();

            return Json(list);
        }
    }
}
