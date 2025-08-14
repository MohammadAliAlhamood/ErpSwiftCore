using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductTaxModels;
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.IService.IProductsService;
namespace ErpSwiftCore.Web.Controllers.CompanyControllers.ProductsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class ProductTaxController : Controller
    {
        private readonly IProductTaxService _taxService;
        private readonly ISelectListService _selectListService;
        private readonly IMapper _mapper;

        public ProductTaxController(
            IProductTaxService taxService,
            ISelectListService selectListService,
            IMapper mapper)
        {
            _taxService = taxService ?? throw new ArgumentNullException(nameof(taxService));
            _selectListService = selectListService ?? throw new ArgumentNullException(nameof(selectListService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _taxService.GetAllAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب الضرائب.";
                return View(Enumerable.Empty<ProductTaxDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<ProductTaxDto>>(resp.Result.ToString())
                       ?? new List<ProductTaxDto>();

            return View(list);
        }
 

        
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var resp = await _taxService.GetByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على الضريبة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductTaxDto>(resp.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات الضريبة.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new ProductTaxViewModel
            {
                ProductTax = new ProductTaxDto(),
                ProductList = await _selectListService.GetProductSelectListAsync()
            };

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _taxService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على الضريبة.";
                    return RedirectToAction(nameof(Index));
                }

                vm.ProductTax = JsonConvert
                    .DeserializeObject<ProductTaxDto>(resp.Result.ToString())
                    ?? new ProductTaxDto();

                // re-select saved value
                vm.ProductList = await _selectListService
                    .GetProductSelectListAsync(vm.ProductTax.ProductId);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductTaxViewModel vm)
        {
            // repopulate products with selected
            vm.ProductList = await _selectListService
                .GetProductSelectListAsync(vm.ProductTax.ProductId);

            if (!ModelState.IsValid)
                return View(vm);

            APIResponseDto resp;
            bool isCreate = vm.ProductTax.ID == Guid.Empty;

            if (isCreate)
            {
                var dto = _mapper.Map<ProductTaxCreateDto>(vm.ProductTax);
                resp = await _taxService.CreateAsync(dto);
            }
            else
            {
                var dto = _mapper.Map<ProductTaxUpdateDto>(vm.ProductTax);
                resp = await _taxService.UpdateAsync(dto);
            }

            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? (isCreate ? "تم إنشاء الضريبة بنجاح." : "تم تحديث الضريبة بنجاح.")
                    : resp?.ErrorMessages.FirstOrDefault() ?? (isCreate ? "فشل إنشاء الضريبة." : "فشل تحديث الضريبة.");

            if (resp == null || !resp.IsSuccess)
                return View(vm);

            return RedirectToAction(nameof(Index));
        }

        #region Delete

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _taxService.DeleteAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف الضريبة بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف الضريبة.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي ضريبة للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _taxService.DeleteRangeAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف مجموعة الضرائب بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة الضرائب.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> BulkDelete(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي ضريبة للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _taxService.BulkDeleteAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف الضريبة بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل الحذف بالجملة.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var resp = await _taxService.DeleteAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف جميع الضرائب بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع الضرائب.";
            return RedirectToAction(nameof(Index));
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> WithProduct(Guid id)
        {
            var resp = await _taxService.GetWithProductAsync(id);
            var dto = JsonConvert.DeserializeObject<ProductTaxDto>(resp?.Result.ToString() ?? "");
            return View("Details", dto);
        }
    }
}
