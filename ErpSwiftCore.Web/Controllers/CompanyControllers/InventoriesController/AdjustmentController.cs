using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.AdjustmentModels;
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.IService.IInventoriesService;
namespace ErpSwiftCore.Web.Controllers.CompanyControllers.InventoriesController
{
    [Authorize(Roles = SD.Role_Company)]
    public class AdjustmentController : Controller
    {
        private readonly IAdjustmentService _adjustmentService;
        private readonly ISelectListService _SelectList; 

        public AdjustmentController(
            IAdjustmentService adjustmentService,
            ISelectListService productSelectList )
        {
            _adjustmentService = adjustmentService
                ?? throw new ArgumentNullException(nameof(adjustmentService));
            _SelectList = productSelectList
                ?? throw new ArgumentNullException(nameof(productSelectList)); 
        }
         
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _adjustmentService.GetAllAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب جميع تعديلات المخزون.";
                return View(Enumerable.Empty<InventoryAdjustmentDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryAdjustmentDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryAdjustmentDto>();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _adjustmentService.GetByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على التعديل المطلوب.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryAdjustmentDto>(apiResponse.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات التعديل.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new InventoryAdjustmentViewModel
            {
                InventoryAdjustment = new InventoryAdjustmentDto
                {
                    AdjustmentDate = DateTime.Now
                },
                ProductList = await _SelectList.GetProductSelectListAsync(),
                WarehouseList = await _SelectList.GetWarehouseSelectListAsync()
            };

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _adjustmentService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على التعديل المطلوب.";
                    return RedirectToAction(nameof(Index));
                }

                var dto = JsonConvert
                    .DeserializeObject<InventoryAdjustmentDto>(resp.Result.ToString())
                    ?? new InventoryAdjustmentDto();

                vm.InventoryAdjustment = dto;
                vm.ProductList = await _SelectList.GetProductSelectListAsync(dto.ProductID);
                vm.WarehouseList = await _SelectList.GetWarehouseSelectListAsync(dto.WarehouseID);
            }

            return View(vm);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(InventoryAdjustmentViewModel vm)
        {
            // always repopulate selects
            vm.ProductList = await _SelectList.GetProductSelectListAsync(vm.InventoryAdjustment.ProductID);
            vm.WarehouseList = await _SelectList.GetWarehouseSelectListAsync(vm.InventoryAdjustment.WarehouseID);

            // Validation
            if (vm.InventoryAdjustment.ProductID == Guid.Empty)
                ModelState.AddModelError(nameof(vm.InventoryAdjustment.ProductID), "الرجاء اختيار منتج.");
            if (vm.InventoryAdjustment.WarehouseID == Guid.Empty)
                ModelState.AddModelError(nameof(vm.InventoryAdjustment.WarehouseID), "الرجاء اختيار مستودع.");
            if (vm.InventoryAdjustment.QuantityChanged == 0)
                ModelState.AddModelError(nameof(vm.InventoryAdjustment.QuantityChanged), "يجب أن يكون التغيير غير صفري.");
            if (string.IsNullOrWhiteSpace(vm.InventoryAdjustment.Reason))
                ModelState.AddModelError(nameof(vm.InventoryAdjustment.Reason), "يرجى إدخال سبب التعديل.");

            if (!ModelState.IsValid)
                return View(vm);

            APIResponseDto? apiResponse;

            // Create
            if (vm.InventoryAdjustment.ID == Guid.Empty)
            {
                var createDto = new CreateInventoryAdjustmentDto
                {
                    ProductId = vm.InventoryAdjustment.ProductID,
                    WarehouseId = vm.InventoryAdjustment.WarehouseID,
                    QuantityChanged = vm.InventoryAdjustment.QuantityChanged,
                    Reason = vm.InventoryAdjustment.Reason
                };
                apiResponse = await _adjustmentService.CreateAsync(createDto);

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    // show first API error via toaster
                    TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء التعديل.";
                    return View(vm);
                }

                TempData["success"] = "تم إنشاء تعديل المخزون بنجاح.";
            }
            // Update
            else
            {
                var updateDto = new UpdateInventoryAdjustmentDto
                {
                    Id = vm.InventoryAdjustment.ID,
                    QuantityChanged = vm.InventoryAdjustment.QuantityChanged,
                    Reason = vm.InventoryAdjustment.Reason,
                    AdjustmentDate = vm.InventoryAdjustment.AdjustmentDate
                };
                apiResponse = await _adjustmentService.UpdateRangeAsync(
                    new UpdateInventoryAdjustmentsRangeDto { Adjustments = new[] { updateDto } }
                );

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث التعديل.";
                    return View(vm);
                }

                TempData["success"] = "تم تحديث التعديل بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }
        // POST: /Adjustment/Delete/{id}
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiResponse = await _adjustmentService.DeleteAsync(id);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف التعديل بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف التعديل.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Adjustment/DeleteRange
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي تعديلات للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var apiResponse = await _adjustmentService.DeleteRangeAsync(ids);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف مجموعة التعديلات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة التعديلات.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Adjustment/DeleteAll
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var apiResponse = await _adjustmentService.DeleteAllAsync();
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم حذف جميع التعديلات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع التعديلات.";
            return RedirectToAction(nameof(Index));
        }







        // POST: /Adjustment/Restore/{id}
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            var apiResponse = await _adjustmentService.RestoreAsync(id);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم استرجاع التعديل بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل استرجاع التعديل.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Adjustment/RestoreRange
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreRange(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي تعديلات للاسترجاع.";
                return RedirectToAction(nameof(Index));
            }

            var apiResponse = await _adjustmentService.RestoreRangeAsync(ids);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم استرجاع مجموعة التعديلات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل استرجاع مجموعة التعديلات.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Adjustment/RestoreAll
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreAll()
        {
            var apiResponse = await _adjustmentService.RestoreAllAsync();
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم استرجاع جميع التعديلات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل استرجاع جميع التعديلات.";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Adjustment/ByProduct/{productId}
        [HttpGet]
        public async Task<IActionResult> ByProduct(Guid productId, Guid? warehouseId = null)
        {
            var apiResponse = await _adjustmentService.GetByProductAsync(productId, warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب التعديلات للمنتج.";
                return View(Enumerable.Empty<InventoryAdjustmentDto>());
            }
            var list = JsonConvert
                .DeserializeObject<IEnumerable<InventoryAdjustmentDto>>(apiResponse.Result.ToString())
                ?? Enumerable.Empty<InventoryAdjustmentDto>();
            return View("Index", list);
        }

        // GET: /Adjustment/ByWarehouse/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> ByWarehouse(Guid warehouseId)
        {
            var apiResponse = await _adjustmentService.GetByWarehouseAsync(warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب التعديلات للمستودع.";
                return View(Enumerable.Empty<InventoryAdjustmentDto>());
            }
            var list = JsonConvert
                .DeserializeObject<IEnumerable<InventoryAdjustmentDto>>(apiResponse.Result.ToString())
                ?? Enumerable.Empty<InventoryAdjustmentDto>();
            return View("Index", list);
        }

        // GET: /Adjustment/ByDateRange?from={from}&to={to}&warehouseId={warehouseId}
        [HttpGet]
        public async Task<IActionResult> ByDateRange(DateTime from, DateTime to, Guid? warehouseId = null)
        {
            var apiResponse = await _adjustmentService.GetByDateRangeAsync(from, to, warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب التعديلات ضمن النطاق الزمني.";
                return View(Enumerable.Empty<InventoryAdjustmentDto>());
            }
            var list = JsonConvert
                .DeserializeObject<IEnumerable<InventoryAdjustmentDto>>(apiResponse.Result.ToString())
                ?? Enumerable.Empty<InventoryAdjustmentDto>();
            return View("Index", list);
        }

        // GET: /Adjustment/CountsByReason?warehouseId={warehouseId}&from={from}&to={to}
        [HttpGet]
        public async Task<IActionResult> CountsByReason(Guid? warehouseId, DateTime? from, DateTime? to)
        {
            var apiResponse = await _adjustmentService.GetCountsByReasonAsync(warehouseId, from, to);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب إحصائيات الأسباب.";
                return View(Enumerable.Empty<AdjustmentReasonCountDto>());
            }
            var list = JsonConvert
                .DeserializeObject<IEnumerable<AdjustmentReasonCountDto>>(apiResponse.Result.ToString())
                ?? Enumerable.Empty<AdjustmentReasonCountDto>();
            return View(list);
        }

        // GET: /Adjustment/LastAdjustment?productId={productId}&warehouseId={warehouseId}
        [HttpGet]
        public async Task<IActionResult> LastAdjustment(Guid productId, Guid warehouseId)
        {
            var apiResponse = await _adjustmentService.GetLastAdjustmentAsync(productId, warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب آخر تعديل.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryAdjustmentDto>(apiResponse.Result.ToString())
                ?? new InventoryAdjustmentDto();
            return View("Details", dto);
        }

        // GET: /Adjustment/CurrentStock?productId={productId}&warehouseId={warehouseId}
        [HttpGet]
        public async Task<IActionResult> CurrentStock(Guid productId, Guid warehouseId)
        {
            var apiResponse = await _adjustmentService.GetCurrentStockAsync(productId, warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب الرصيد الحالي.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryAdjustmentDto>(apiResponse.Result.ToString())
                ?? new InventoryAdjustmentDto();
            return View("Details", dto);
        }

        // GET: /Adjustment/SumQuantity?productId={productId}&from={from}&to={to}
        [HttpGet]
        public async Task<IActionResult> SumQuantity(Guid productId, DateTime from, DateTime to)
        {
            var apiResponse = await _adjustmentService.SumQuantityByDateRangeAsync(productId, from, to);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل حساب مجموع الكميات.";
                return RedirectToAction(nameof(Index));
            }

            var total = JsonConvert
                .DeserializeObject<int>(apiResponse.Result.ToString());
            return View(total);
        }

        // POST: /Adjustment/UpdateRange
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRange(UpdateInventoryAdjustmentsRangeDto dto)
        {
            var apiResponse = await _adjustmentService.UpdateRangeAsync(dto);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم تحديث مجموعة التعديلات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث مجموعة التعديلات.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Adjustment/UpdateReason
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReason(UpdateInventoryAdjustmentReasonByDateRangeDto dto)
        {
            var apiResponse = await _adjustmentService.UpdateReasonAsync(dto);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم تحديث سبب التعديلات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث سبب التعديلات.";
            return RedirectToAction(nameof(Index));
        }
         
    }
}
