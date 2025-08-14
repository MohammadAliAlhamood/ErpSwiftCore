using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.WarehouseModels;
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.IService.IInventoriesService;
namespace ErpSwiftCore.Web.Controllers.CompanyControllers.InventoriesController
{
    [Authorize(Roles = SD.Role_Company)]
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouseService;
        private readonly ISelectListService _SelectList;

        public WarehouseController(
            IWarehouseService warehouseService,
            ISelectListService branchSelectList)
        {
            _warehouseService = warehouseService ?? throw new ArgumentNullException(nameof(warehouseService));
            _SelectList = branchSelectList ?? throw new ArgumentNullException(nameof(branchSelectList));
        }

        // GET: /Warehouse
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _warehouseService.GetAllAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب جميع المستودعات.";
                return View(Enumerable.Empty<WarehouseDto>());
            }
            var list = JsonConvert
                .DeserializeObject<List<WarehouseDto>>(resp.Result.ToString())
                ?? new List<WarehouseDto>();
            return View(list);
        }

        // GET: /Warehouse/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var resp = await _warehouseService.GetByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على المستودع.";
                return RedirectToAction(nameof(Index));
            }
            var dto = JsonConvert
                .DeserializeObject<WarehouseWithInventoriesDto>(resp.Result.ToString())
                ?? throw new InvalidOperationException("Invalid warehouse data.");
            return View(dto);
        }

        // GET & POST: Upsert (Create or Edit)
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new WarehouseViewModel
            {
                Warehouse = new WarehouseDto(),
                BranchList = await _SelectList.GetBranchSelectListAsync()
            };

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _warehouseService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                       ?? "لم يتم العثور على المستودع.";
                    return RedirectToAction(nameof(Index));
                }
                var dto = JsonConvert
                    .DeserializeObject<WarehouseDto>(resp.Result.ToString())
                    ?? new WarehouseDto();
                vm.Warehouse = dto;
                vm.BranchList = await _SelectList.GetBranchSelectListAsync(dto.BranchID);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(WarehouseViewModel vm)
        {
            // repopulate branch list
            vm.BranchList = await _SelectList.GetBranchSelectListAsync(vm.Warehouse.BranchID);

            // validation
            if (string.IsNullOrWhiteSpace(vm.Warehouse.Name))
                ModelState.AddModelError(nameof(vm.Warehouse.Name), "يرجى إدخال اسم المستودع.");
            if (vm.Warehouse.BranchID == Guid.Empty)
                ModelState.AddModelError(nameof(vm.Warehouse.BranchID), "يرجى اختيار الفرع.");
            else
            {
                // check branch validity
                var chk = await _warehouseService.ValidateBranchAsync(new ValidateBranchDto
                {
                    BranchId = vm.Warehouse.BranchID
                });
                if (chk == null || !chk.IsSuccess)
                    ModelState.AddModelError(nameof(vm.Warehouse.BranchID),
                        chk?.ErrorMessages.FirstOrDefault() ?? "فرع غير صالح.");
            }
            // unique name within branch
            if (!string.IsNullOrWhiteSpace(vm.Warehouse.Name))
            {
                var dup = await _warehouseService.CheckNameAsync(new ExistsWarehouseNameDto
                {
                    Name = vm.Warehouse.Name,
                    BranchId = vm.Warehouse.BranchID,
                    ExcludeId = vm.Warehouse.ID == Guid.Empty ? null : vm.Warehouse.ID
                });
                if (dup == null || !dup.IsSuccess)
                    ModelState.AddModelError(nameof(vm.Warehouse.Name),
                        dup?.ErrorMessages.FirstOrDefault() ?? "اسم المستودع مكرر.");
            }
            APIResponseDto? resp;
            if (vm.Warehouse.ID == Guid.Empty)
            {
                var createDto = new CreateWarehouseDto
                {
                    Name = vm.Warehouse.Name,
                    Location = vm.Warehouse.Location,
                    BranchID = vm.Warehouse.BranchID,
                    IsStorage = vm.Warehouse.IsStorage,
                    IsOperational = vm.Warehouse.IsOperational
                };
                resp = await _warehouseService.CreateAsync(createDto);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                       ?? "فشل إنشاء المستودع.";
                    return View(vm);
                }
                TempData["success"] = "تم إنشاء المستودع بنجاح.";
            }
            else
            {
                var updateDto = new UpdateWarehouseDto
                {
                    ID = vm.Warehouse.ID,
                    Name = vm.Warehouse.Name,
                    Location = vm.Warehouse.Location,
                    BranchID = vm.Warehouse.BranchID,
                    IsStorage = vm.Warehouse.IsStorage,
                    IsOperational = vm.Warehouse.IsOperational
                };
                resp = await _warehouseService.UpdateAsync(updateDto);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                       ?? "فشل تحديث المستودع.";
                    return View(vm);
                }
                TempData["success"] = "تم تحديث المستودع بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Warehouse/Delete/{id}
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _warehouseService.DeleteAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف المستودع بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف المستودع.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Warehouse/DeleteRange
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(DeleteWarehousesRangeDto dto)
        {
            if (dto.WarehouseIds == null || !dto.WarehouseIds.Any())
            {
                TempData["error"] = "لم يتم تحديد أي مستودعات للحذف.";
                return RedirectToAction(nameof(Index));
            }
            var resp = await _warehouseService.DeleteRangeAsync(dto);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف مجموعة المستودعات بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة المستودعات.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Warehouse/DeleteAll
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var resp = await _warehouseService.DeleteAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف جميع المستودعات بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع المستودعات.";
            return RedirectToAction(nameof(Index));
        }









        // POST: /Warehouse/Restore/{id}
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(RestoreWarehouseDto dto)
        {
            var resp = await _warehouseService.RestoreAsync(dto);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم استرجاع المستودع بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل استرجاع المستودع.";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreRange(RestoreWarehousesRangeDto dto)
        {
            if (dto.WarehouseIds == null || !dto.WarehouseIds.Any())
            {
                TempData["error"] = "لم يتم تحديد أي مستودعات للاسترجاع.";
                return RedirectToAction(nameof(Index));
            }
            var resp = await _warehouseService.RestoreRangeAsync(dto);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم استرجاع مجموعة المستودعات بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل استرجاع مجموعة المستودعات.";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreAll()
        {
            var resp = await _warehouseService.RestoreAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم استرجاع جميع المستودعات بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل استرجاع جميع المستودعات.";
            return RedirectToAction(nameof(Index));
        }



        // 추가 액션: 아직 구현되지 않은 서비스 메서드들에 대한 컨트롤러 액션
        // ErpSwiftCore.Web.Controllers.CompanyControllers.InventoriesController.WarehouseController

        // GET: /Warehouse/Inventories/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> Inventories(Guid warehouseId)
        {
            var resp = await _warehouseService.GetInventoriesAsync(warehouseId);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب المخزونات للمستودع.";
                return RedirectToAction(nameof(Index));
            }
            var list = JsonConvert
                .DeserializeObject<List<InventoryDto>>(resp.Result.ToString())
                ?? new List<InventoryDto>();
            return View("Details", list);
        }

        // GET: /Warehouse/TotalInventories/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> TotalInventories(Guid warehouseId)
        {
            var resp = await _warehouseService.GetTotalInventoriesAsync(warehouseId);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب إجمالي المخزونات.";
                return RedirectToAction(nameof(Index));
            }
            var dto = JsonConvert
                .DeserializeObject<WarehouseInventoriesCountDto>(resp.Result.ToString())
                ?? new WarehouseInventoriesCountDto();
            return View(dto);
        }

        // GET: /Warehouse/TotalProducts/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> TotalProducts(Guid warehouseId)
        {
            var resp = await _warehouseService.GetTotalProductsAsync(warehouseId);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب إجمالي المنتجات.";
                return RedirectToAction(nameof(Index));
            }
            var dto = JsonConvert
                .DeserializeObject<WarehouseDistinctProductsCountDto>(resp.Result.ToString())
                ?? new WarehouseDistinctProductsCountDto();
            return View(dto);
        }

        // GET: /Warehouse/LowStockCount/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> LowStockCount(Guid warehouseId)
        {
            var resp = await _warehouseService.GetLowStockCountAsync(warehouseId);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب عدد العناصر منخفضة المخزون.";
                return RedirectToAction(nameof(Index));
            }
            var dto = JsonConvert
                .DeserializeObject<WarehouseLowStockCountDto>(resp.Result.ToString())
                ?? new WarehouseLowStockCountDto();
            return View(dto);
        }

        // GET: /Warehouse/OverstockedCount/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> OverstockedCount(Guid warehouseId)
        {
            var resp = await _warehouseService.GetOverstockedCountAsync(warehouseId);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب عدد العناصر فائقة التخزين.";
                return RedirectToAction(nameof(Index));
            }
            var dto = JsonConvert
                .DeserializeObject<WarehouseOverstockCountDto>(resp.Result.ToString())
                ?? new WarehouseOverstockCountDto();
            return View(dto);
        }

        // GET: /Warehouse/AverageStockLevel/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> AverageStockLevel(Guid warehouseId)
        {
            var resp = await _warehouseService.GetAverageStockLevelAsync(warehouseId);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب متوسط مستوى المخزون.";
                return RedirectToAction(nameof(Index));
            }
            var dto = JsonConvert
                .DeserializeObject<WarehouseAverageStockLevelDto>(resp.Result.ToString())
                ?? new WarehouseAverageStockLevelDto();
            return View(dto);
        }

        // GET: /Warehouse/Recent?maxCount=10
        [HttpGet]
        public async Task<IActionResult> Recent(int maxCount = 10)
        {
            var resp = await _warehouseService.GetRecentAsync(maxCount);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب أحدث المستودعات.";
                return RedirectToAction(nameof(Index));
            }
            var list = JsonConvert
                .DeserializeObject<List<RecentWarehouseDto>>(resp.Result.ToString())
                ?? new List<RecentWarehouseDto>();
            return View(list);
        }

        // GET: /Warehouse/InventoryCountPerWarehouse
        [HttpGet]
        public async Task<IActionResult> InventoryCountPerWarehouse()
        {
            var resp = await _warehouseService.GetInventoryCountPerWarehouseAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب إحصائيات المخزونات لكل مستودع.";
                return RedirectToAction(nameof(Index));
            }
            var list = JsonConvert
                .DeserializeObject<List<InventoryCountPerWarehouseDto>>(resp.Result.ToString())
                ?? new List<InventoryCountPerWarehouseDto>();
            return View(list);
        }

        // GET: /Warehouse/TotalWarehouses
        [HttpGet]
        public async Task<IActionResult> TotalWarehouses()
        {
            var resp = await _warehouseService.GetTotalWarehousesAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب إجمالي عدد المستودعات.";
                return RedirectToAction(nameof(Index));
            }
            var dto = JsonConvert
                .DeserializeObject<WarehouseCountDto>(resp.Result.ToString())
                ?? new WarehouseCountDto();
            return View(dto);
        }

        // GET: /Warehouse/CountByBranch/{branchId}
        [HttpGet]
        public async Task<IActionResult> CountByBranch(Guid branchId)
        {
            var resp = await _warehouseService.GetCountByBranchAsync(branchId);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب عدد المستودعات حسب الفرع.";
                return RedirectToAction(nameof(Index));
            }
            var dto = JsonConvert
                .DeserializeObject<WarehouseCountDto>(resp.Result.ToString())
                ?? new WarehouseCountDto();
            return View(dto);
        }

        // GET: /Warehouse/Operational
        [HttpGet]
        public async Task<IActionResult> Operational()
        {
            var resp = await _warehouseService.GetOperationalAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب المستودعات العاملة.";
                return RedirectToAction(nameof(Index));
            }
            var list = JsonConvert
                .DeserializeObject<List<WarehouseDto>>(resp.Result.ToString())
                ?? new List<WarehouseDto>();
            return View("Index", list);
        }

        // GET: /Warehouse/StorageOnly
        [HttpGet]
        public async Task<IActionResult> StorageOnly()
        {
            var resp = await _warehouseService.GetStorageOnlyAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب المستودعات التخزينية.";
                return RedirectToAction(nameof(Index));
            }
            var list = JsonConvert
                .DeserializeObject<List<WarehouseDto>>(resp.Result.ToString())
                ?? new List<WarehouseDto>();
            return View("Index", list);
        }

        // GET: /Warehouse/ByBranch/{branchId}
        [HttpGet]
        public async Task<IActionResult> ByBranch(Guid branchId)
        {
            var resp = await _warehouseService.GetByBranchAsync(branchId);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب المستودعات حسب الفرع.";
                return RedirectToAction(nameof(Index));
            }
            var list = JsonConvert
                .DeserializeObject<List<WarehouseDto>>(resp.Result.ToString())
                ?? new List<WarehouseDto>();
            return View("Index", list);
        }

        // POST: /Warehouse/ByIds
        [HttpPost]
        public async Task<IActionResult> ByIds([FromBody] List<Guid> ids)
        {
            var resp = await _warehouseService.GetByIdsAsync(ids);
            if (resp == null || !resp.IsSuccess)
                return Json(new { success = false, message = resp?.ErrorMessages.FirstOrDefault() });

            return Json(resp.Result);
        }

        // GET: /Warehouse/SoftDeleted/{id}
        [HttpGet]
        public async Task<IActionResult> SoftDeletedDetails(Guid id)
        {
            var resp = await _warehouseService.GetSoftDeletedByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على المستودع المحذوف.";
                return RedirectToAction(nameof(Index));
            }
            var dto = JsonConvert
                .DeserializeObject<WarehouseDto>(resp.Result.ToString())
                ?? new WarehouseDto();
            return View("Details", dto);
        }

    }
}
