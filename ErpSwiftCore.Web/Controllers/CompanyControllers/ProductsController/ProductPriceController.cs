using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductPriceModels;
using Newtonsoft.Json; 
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.IService.IProductsService;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.ProductsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class ProductPriceController : Controller
    {
        private readonly IProductPriceService _priceService;
        private readonly ISelectListService _selectListService;
        private readonly IMapper _mapper;

        public ProductPriceController(IProductPriceService priceService, ISelectListService selectListService, IMapper mapper)
        {
            _priceService = priceService ?? throw new ArgumentNullException(nameof(priceService));
            _selectListService = selectListService ?? throw new ArgumentNullException(nameof(selectListService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _priceService.GetAllAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "حدث خطأ أثناء جلب الأسعار.";
                return View(Enumerable.Empty<ProductPriceDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductPriceDto>>(resp.Result.ToString())
                       ?? new List<ProductPriceDto>();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var resp = await _priceService.GetByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على السعر المطلوب.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductPriceDto>(resp.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات السعر.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new ProductPriceViewModel
            {
                ProductPrice = new ProductPriceDto(),
                ProductList = await _selectListService.GetProductSelectListAsync(),
                CurrencyList = await _selectListService.GetCurrencySelectListAsync()
            };

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _priceService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على السعر المطلوب.";
                    return RedirectToAction(nameof(Index));
                }

                vm.ProductPrice = JsonConvert
                    .DeserializeObject<ProductPriceDto>(resp.Result.ToString())
                    ?? new ProductPriceDto();

                // re‑select saved values
                vm.ProductList = await _selectListService.GetProductSelectListAsync(vm.ProductPrice.ProductId);
                vm.CurrencyList = await _selectListService.GetCurrencySelectListAsync(vm.ProductPrice.CurrencyId);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductPriceViewModel vm)
        {
            // repopulate lists with selected values
            vm.ProductList = await _selectListService.GetProductSelectListAsync(vm.ProductPrice.ProductId);
            vm.CurrencyList = await _selectListService.GetCurrencySelectListAsync(vm.ProductPrice.CurrencyId);

            if (!ModelState.IsValid)
                return View(vm);

            APIResponseDto resp;
            bool isCreate = vm.ProductPrice.ID == Guid.Empty;

            if (isCreate)
            {
                var dto = _mapper.Map<ProductPriceCreateDto>(vm.ProductPrice);
                resp = await _priceService.CreateAsync(dto);
            }
            else
            {
                var dto = _mapper.Map<ProductPriceUpdateDto>(vm.ProductPrice);
                resp = await _priceService.UpdateAsync(dto);
            }

            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? (isCreate ? "تم إنشاء السعر بنجاح." : "تم تحديث السعر بنجاح.")
                    : resp?.ErrorMessages.FirstOrDefault() ?? (isCreate ? "فشل إنشاء السعر." : "فشل تحديث السعر.");

            if (resp == null || !resp.IsSuccess)
                return View(vm);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _priceService.DeleteAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف السعر بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف السعر.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي سعر للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _priceService.DeleteRangeAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف مجموعة الأسعار بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة الأسعار.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var resp = await _priceService.DeleteAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف جميع الأسعار بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع الأسعار.";

            return RedirectToAction(nameof(Index));
        }
    }
}
