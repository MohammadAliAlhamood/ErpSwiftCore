using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using ErpSwiftCore.Web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.IService.IProductsService;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.ProductsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ISelectListService _selectListService;
        public ProductController(IProductService productService, IMapper mapper, ISelectListService selectListService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _selectListService = selectListService ?? throw new ArgumentNullException(nameof(selectListService));
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _productService.GetAllAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب قائمة المنتجات.";
                return View(Enumerable.Empty<ProductDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductDto>>(apiResponse.Result.ToString())
                       ?? new List<ProductDto>();

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _productService.GetByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على المنتج المطلوب.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductDto>(apiResponse.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات المنتج.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new ProductViewModel
            {
                Product = new ProductDto(),
                UnitOfMeasurementList = await _selectListService.GetUnitSelectListAsync(),
                CategoryList = await _selectListService.GetCategorySelectListAsync()
            };

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _productService.GetByIdAsync(id.Value);
                if (resp?.IsSuccess == true && resp.Result != null)
                {
                    vm.Product = JsonConvert
                        .DeserializeObject<ProductDto>(resp.Result.ToString())
                        ?? new ProductDto();

                    // re‑select the saved values
                    vm.UnitOfMeasurementList = await _selectListService
                        .GetUnitSelectListAsync(vm.Product.UnitOfMeasurementId);

                    vm.CategoryList = await _selectListService
                        .GetCategorySelectListAsync(vm.Product.CategoryId);
                }
                else
                {
                    TempData["error"] = "تعذّر العثور على المنتج المحدد.";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(vm);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductViewModel vm)
        {
            // repopulate lists before validation
            vm.UnitOfMeasurementList = await _selectListService.GetUnitSelectListAsync(vm.Product.UnitOfMeasurementId);
            vm.CategoryList = await _selectListService.GetCategorySelectListAsync(vm.Product.CategoryId);

          
            APIResponseDto apiResponse;
            bool isCreate = vm.Product.ID == Guid.Empty;

            if (isCreate)
            {
                var dto = _mapper.Map<ProductCreateDto>(vm.Product);
                apiResponse = await _productService.CreateAsync(dto);

                TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                    apiResponse?.IsSuccess == true
                        ? "تم إنشاء المنتج بنجاح."
                        : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء المنتج.";
            }
            else
            {
                var dto = _mapper.Map<ProductUpdateDto>(vm.Product);
                apiResponse = await _productService.UpdateAsync(dto);

                TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                    apiResponse?.IsSuccess == true
                        ? "تم تحديث المنتج بنجاح."
                        : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث المنتج.";
            }

            if (apiResponse == null || !apiResponse.IsSuccess)
                return View(vm);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dto = new ProductDeleteDto { ProductId = id };
            var apiResponse = await _productService.DeleteAsync(dto);

            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف المنتج بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف المنتج.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي منتج للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var dto = new ProductDeleteRangeDto { ProductIds = ids };
            var apiResponse = await _productService.DeleteRangeAsync(dto);

            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف مجموعة المنتجات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة المنتجات.";

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> ByCategory(Guid categoryId)
        {
            var apiResponse = await _productService.GetByCategoryAsync(categoryId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على منتجات ضمن هذه الفئة.";
                return RedirectToAction(nameof(Index));
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductDto>>(apiResponse.Result.ToString())
                ?? new List<ProductDto>();

            // نعيد إلى نفس الـ View الخاص بالـ Index
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> ByBundle(Guid bundleId)
        {
            var apiResponse = await _productService.GetByBundleAsync(bundleId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على منتجات ضمن هذه الحزمة.";
                return RedirectToAction(nameof(Index));
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductDto>>(apiResponse.Result.ToString())
                ?? new List<ProductDto>();

            return View("Index", list);
        }

        [HttpGet]
        public async Task<IActionResult> ByTax(Guid taxId)
        {
            var apiResponse = await _productService.GetByTaxAsync(taxId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على منتجات بهذا الضريبة.";
                return RedirectToAction(nameof(Index));
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductDto>>(apiResponse.Result.ToString())
                ?? new List<ProductDto>();

            return View("Index", list);
        }

        [HttpGet]
        public async Task<IActionResult> ByUnit(Guid unitId)
        {
            var apiResponse = await _productService.GetByUnitAsync(unitId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على منتجات بهذه الوحدة.";
                return RedirectToAction(nameof(Index));
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductDto>>(apiResponse.Result.ToString())
                ?? new List<ProductDto>();

            return View("Index", list);
        }

        [HttpGet]
        public async Task<IActionResult> ByProductType(Guid productTypeId)
        {
            var apiResponse = await _productService.GetByProductTypeAsync(productTypeId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على منتجات بهذا النوع.";
                return RedirectToAction(nameof(Index));
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductDto>>(apiResponse.Result.ToString())
                ?? new List<ProductDto>();

            return View("Index", list);
        }

        [HttpGet]
        public async Task<IActionResult> WithBundles(Guid id)
        {
            var apiResponse = await _productService.GetWithBundlesAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب المنتج مع الحزم.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductDto>(apiResponse.Result.ToString());

            return View("Details", dto);
        }

        [HttpGet]
        public async Task<IActionResult> WithPrices(Guid id)
        {
            var apiResponse = await _productService.GetWithPricesAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب المنتج مع الأسعار.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductDto>(apiResponse.Result.ToString());

            return View("Details", dto);
        }

        [HttpGet]
        public async Task<IActionResult> WithTaxes(Guid id)
        {
            var apiResponse = await _productService.GetWithTaxesAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب المنتج مع الضرائب.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductDto>(apiResponse.Result.ToString());

            return View("Details", dto);
        }

        [HttpGet]
        public async Task<IActionResult> WithUnits(Guid id)
        {
            var apiResponse = await _productService.GetWithUnitsAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب المنتج مع الوحدات.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductDto>(apiResponse.Result.ToString());

            return View("Details", dto);
        }
    }
}
