using AutoMapper;
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.IService.IProductsService;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.ProductsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly ISelectListService _selectListService;
        private readonly IMapper _mapper;

        public ProductCategoryController(
            IProductCategoryService productCategoryService,
            ISelectListService selectListService,
            IMapper mapper)
        {
            _productCategoryService = productCategoryService
                ?? throw new ArgumentNullException(nameof(productCategoryService));
            _selectListService = selectListService
                ?? throw new ArgumentNullException(nameof(selectListService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _productCategoryService.GetAllAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب فئات المنتجات.";
                return View(Enumerable.Empty<ProductCategoryDto>());
            }

            var list = JsonConvert.DeserializeObject<List<ProductCategoryDto>>(apiResponse.Result.ToString())
                       ?? new List<ProductCategoryDto>();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _productCategoryService.GetByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على الفئة المطلوبة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductCategoryDto>(apiResponse.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات الفئة.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new ProductCategoryViewModel
            {
                ProductCategory = new ProductCategoryDto(),
                ParentCategoryList = await _selectListService
                                          .GetCategorySelectListAsync()
            };

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _productCategoryService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                       ?? "لم يتم العثور على الفئة المطلوبة.";
                    return RedirectToAction(nameof(Index));
                }

                var dto = JsonConvert
                    .DeserializeObject<ProductCategoryDto>(resp.Result.ToString())
                    ?? new ProductCategoryDto();

                vm.ProductCategory = dto;
                vm.ParentCategoryList = await _selectListService
                                            .GetCategorySelectListAsync(dto.ParentCategoryId);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductCategoryViewModel vm)
        {
            // Always repopulate the select lists before validation
            vm.ParentCategoryList = await _selectListService
                                         .GetCategorySelectListAsync(vm.ProductCategory.ParentCategoryId);
 

            APIResponseDto? apiResponse;

            if (vm.ProductCategory.ID == Guid.Empty)
            {
                var createDto = _mapper.Map<ProductCategoryCreateDto>(vm.ProductCategory);
                apiResponse = await _productCategoryService.CreateAsync(createDto);

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء الفئة."
                    );
                    return View(vm);
                }

                TempData["success"] = "تم إنشاء الفئة بنجاح.";
            }
            else
            {
                var updateDto = _mapper.Map<ProductCategoryUpdateDto>(vm.ProductCategory);
                apiResponse = await _productCategoryService.UpdateAsync(updateDto);

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث الفئة."
                    );
                    return View(vm);
                }

                TempData["success"] = "تم تحديث الفئة بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiResponse = await _productCategoryService.DeleteAsync(id);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف الفئة بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف الفئة.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي فئة للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var apiResponse = await _productCategoryService.DeleteRangeAsync(ids);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف مجموعة الفئات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة الفئات.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var apiResponse = await _productCategoryService.DeleteAllAsync();
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف جميع الفئات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع الفئات.";

            return RedirectToAction(nameof(Index));
        }
    }
}