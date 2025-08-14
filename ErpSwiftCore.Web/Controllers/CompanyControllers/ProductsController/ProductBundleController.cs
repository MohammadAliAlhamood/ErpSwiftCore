using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductBundleModels;
using ErpSwiftCore.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.IService.IProductsService;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.ProductsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class ProductBundleController : Controller
    {
        private readonly IProductBundleService _bundleService;
        private readonly IMapper _mapper;
        private readonly ISelectListService _selectListService;

        public ProductBundleController(
            IProductBundleService bundleService,
            IMapper mapper,
            ISelectListService selectListService)
        {
            _bundleService = bundleService ?? throw new ArgumentNullException(nameof(bundleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _selectListService = selectListService ?? throw new ArgumentNullException(nameof(selectListService));
        }

        #region List & Filters

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _bundleService.GetAllAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب الحزم.";
                return View(Enumerable.Empty<ProductBundleDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductBundleDto>>(resp.Result.ToString())
                       ?? new List<ProductBundleDto>();

            return View(list);
        }

   
 

        #endregion

        #region Details

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var resp = await _bundleService.GetByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على الحزمة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductBundleDto>(resp.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات الحزمة.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        #endregion

        #region Upsert

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new ProductBundleViewModel
            {
                ProductBundle = new ProductBundleDto(),
                ParentProductList = await _selectListService.GetProductSelectListAsync(),
                ComponentProductList = await _selectListService.GetProductSelectListAsync(),
                UnitOfMeasurementList = await _selectListService.GetUnitSelectListAsync()
            };

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _bundleService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على بيانات الحزمة.";
                    return RedirectToAction(nameof(Index));
                }

                vm.ProductBundle = JsonConvert
                    .DeserializeObject<ProductBundleDto>(resp.Result.ToString())
                    ?? new ProductBundleDto();

                // re‑select saved values
                vm.ParentProductList = await _selectListService.GetProductSelectListAsync(vm.ProductBundle.ParentProductId);
                vm.ComponentProductList = await _selectListService.GetProductSelectListAsync(vm.ProductBundle.ComponentProductId);
                vm.UnitOfMeasurementList = await _selectListService.GetUnitSelectListAsync(vm.ProductBundle.UnitOfMeasurementId);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductBundleViewModel vm)
        {
            // repopulate lists with selected values
            vm.ParentProductList = await _selectListService.GetProductSelectListAsync(vm.ProductBundle.ParentProductId);
            vm.ComponentProductList = await _selectListService.GetProductSelectListAsync(vm.ProductBundle.ComponentProductId);
            vm.UnitOfMeasurementList = await _selectListService.GetUnitSelectListAsync(vm.ProductBundle.UnitOfMeasurementId);

            if (!ModelState.IsValid)
                return View(vm);

            APIResponseDto? resp;
            if (vm.ProductBundle.ID == Guid.Empty)
            {
                var dto = _mapper.Map<ProductBundleCreateDto>(vm.ProductBundle);
                resp = await _bundleService.CreateAsync(dto);
                TempData[resp?.IsSuccess == true ? "success" : "error"] =
                    resp?.IsSuccess == true
                        ? "تم إنشاء الحزمة بنجاح."
                        : resp?.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء الحزمة.";
            }
            else
            {
                var dto = _mapper.Map<ProductBundleUpdateDto>(vm.ProductBundle);
                resp = await _bundleService.UpdateAsync(dto);
                TempData[resp?.IsSuccess == true ? "success" : "error"] =
                    resp?.IsSuccess == true
                        ? "تم تحديث الحزمة بنجاح."
                        : resp?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث الحزمة.";
            }

            if (resp == null || !resp.IsSuccess)
                return View(vm);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _bundleService.DeleteAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف الحزمة بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف الحزمة.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي حزمة للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _bundleService.DeleteRangeAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف مجموعة الحزم بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة الحزم.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var resp = await _bundleService.DeleteAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف جميع الحزم بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع الحزم.";

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Counts & Related

        [HttpGet]
        public async Task<IActionResult> WithParent(Guid id)
        {
            var resp = await _bundleService.GetWithParentAsync(id);
            var dto = JsonConvert.DeserializeObject<ProductBundleDto>(resp?.Result.ToString() ?? "");
            return View("Details", dto);
        }

        [HttpGet]
        public async Task<IActionResult> WithComponent(Guid id)
        {
            var resp = await _bundleService.GetWithComponentAsync(id);
            var dto = JsonConvert.DeserializeObject<ProductBundleDto>(resp?.Result.ToString() ?? "");
            return View("Details", dto);
        }

        [HttpGet]
        public async Task<IActionResult> WithUnit(Guid id)
        {
            var resp = await _bundleService.GetWithUnitAsync(id);
            var dto = JsonConvert.DeserializeObject<ProductBundleDto>(resp?.Result.ToString() ?? "");
            return View("Details", dto);
        }

        #endregion
    }
}
