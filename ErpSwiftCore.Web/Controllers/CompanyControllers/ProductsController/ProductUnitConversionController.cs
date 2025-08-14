using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductUnitConversionModels;
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.IService.IProductsService;
namespace ErpSwiftCore.Web.Controllers.CompanyControllers.ProductsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class ProductUnitConversionController : Controller
    {
        private readonly IProductUnitConversionService _conversionService;
        private readonly ISelectListService _selectListService;
        private readonly IMapper _mapper;

        public ProductUnitConversionController(
            IProductUnitConversionService conversionService,
            ISelectListService selectListService,
            IMapper mapper)
        {
            _conversionService = conversionService ?? throw new ArgumentNullException(nameof(conversionService));
            _selectListService = selectListService ?? throw new ArgumentNullException(nameof(selectListService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _conversionService.GetAllAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب تحويلات الوحدات.";
                return View(Enumerable.Empty<ProductUnitConversionDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductUnitConversionDto>>(resp.Result.ToString())
                       ?? new List<ProductUnitConversionDto>();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> ByProduct(Guid productId)
            => await FilterAndReturn(
                () => _conversionService.GetByProductAsync(productId),
                "فشل جلب التحويلات للمنتج المحدد.");

        private async Task<IActionResult> FilterAndReturn(
            Func<Task<APIResponseDto>> fetch,
            string errorMsg)
        {
            var resp = await fetch();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? errorMsg;
                return View("Index", Enumerable.Empty<ProductUnitConversionDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductUnitConversionDto>>(resp.Result.ToString())
                       ?? new List<ProductUnitConversionDto>();

            return View("Index", list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var resp = await _conversionService.GetByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على التحويل.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductUnitConversionDto>(resp.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات التحويل.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new ProductUnitConversionViewModel
            {
                ProductUnitConversion = new ProductUnitConversionDto(),
                ProductList = await _selectListService.GetProductSelectListAsync(),
                FromUnitList = await _selectListService.GetUnitSelectListAsync(),
                ToUnitList = await _selectListService.GetUnitSelectListAsync()
            };

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _conversionService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على بيانات التحويل.";
                    return RedirectToAction(nameof(Index));
                }

                vm.ProductUnitConversion = JsonConvert
                    .DeserializeObject<ProductUnitConversionDto>(resp.Result.ToString())
                    ?? new ProductUnitConversionDto();

                // re‑select saved values
                vm.ProductList = await _selectListService.GetProductSelectListAsync(vm.ProductUnitConversion.ProductId);
                vm.FromUnitList = await _selectListService.GetUnitSelectListAsync(vm.ProductUnitConversion.FromUnitId);
                vm.ToUnitList = await _selectListService.GetUnitSelectListAsync(vm.ProductUnitConversion.ToUnitId);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductUnitConversionViewModel vm)
        {
            // repopulate lists with selected values
            vm.ProductList = await _selectListService.GetProductSelectListAsync(vm.ProductUnitConversion.ProductId);
            vm.FromUnitList = await _selectListService.GetUnitSelectListAsync(vm.ProductUnitConversion.FromUnitId);
            vm.ToUnitList = await _selectListService.GetUnitSelectListAsync(vm.ProductUnitConversion.ToUnitId);

            if (!ModelState.IsValid)
                return View(vm);

            APIResponseDto resp;
            bool isCreate = vm.ProductUnitConversion.ID == Guid.Empty;

            if (isCreate)
            {
                var dto = _mapper.Map<ProductUnitConversionCreateDto>(vm.ProductUnitConversion);
                resp = await _conversionService.CreateAsync(dto);
            }
            else
            {
                var dto = _mapper.Map<ProductUnitConversionUpdateDto>(vm.ProductUnitConversion);
                resp = await _conversionService.UpdateAsync(dto);
            }

            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? (isCreate ? "تم إنشاء التحويل بنجاح." : "تم تحديث التحويل بنجاح.")
                    : resp?.ErrorMessages.FirstOrDefault() ?? (isCreate ? "فشل إنشاء التحويل." : "فشل تحديث التحويل.");

            if (resp == null || !resp.IsSuccess)
                return View(vm);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _conversionService.DeleteAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف التحويل بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف التحويل.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي تحويل للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _conversionService.DeleteRangeAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف مجموعة التحويلات بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة التحويلات.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var resp = await _conversionService.DeleteAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف جميع التحويلات بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع التحويلات.";

            return RedirectToAction(nameof(Index));
        }
    }
}
