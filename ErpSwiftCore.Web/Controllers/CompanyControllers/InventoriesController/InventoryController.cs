using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErpSwiftCore.Web.IService.IInventoriesService;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryModels;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryPolicyModels;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryTransactionModels;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.WarehouseModels;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.InventoriesController
{
    [Authorize(Roles = SD.Role_Company)]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService
                ?? throw new ArgumentNullException(nameof(inventoryService));
        }

        // GET: /Inventory
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _inventoryService.GetAllAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب بيانات المخزون.";
                return View(Enumerable.Empty<InventoryDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryDto>();
            return View(list);
        }

        // GET: /Inventory/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _inventoryService.GetByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على سجل المخزون.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryDto>(apiResponse.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات المخزون.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        // GET: /Inventory/SoftDeletedDetails/{id}
        [HttpGet]
        public async Task<IActionResult> SoftDeletedDetails(Guid id)
        {
            var apiResponse = await _inventoryService.GetSoftDeletedByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على السجل المحذوف.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryDto>(apiResponse.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات السجل المحذوف.";
                return RedirectToAction(nameof(Index));
            }

            return View("Details", dto);
        }


        // GET: /Inventory/Snapshot?productId={productId}&warehouseId={warehouseId}
        [HttpGet]
        public async Task<IActionResult> Snapshot(Guid productId, Guid warehouseId)
        {
            var apiResponse = await _inventoryService.GetSnapshotAsync(productId, warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب بيانات اللقطة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryDto>(apiResponse.Result.ToString())
                ?? new InventoryDto();
            return View(dto);
        }

        // GET: /Inventory/ByProduct/{productId}
        [HttpGet]
        public async Task<IActionResult> ByProduct(Guid productId)
        {
            var apiResponse = await _inventoryService.GetByProductAsync(productId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب بيانات المخزون للمنتج.";
                return View(Enumerable.Empty<InventoryDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryDto>();
            return View(list);
        }

        // GET: /Inventory/ByWarehouse/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> ByWarehouse(Guid warehouseId)
        {
            var apiResponse = await _inventoryService.GetByWarehouseAsync(warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب بيانات المخزون للمستودع.";
                return View(Enumerable.Empty<InventoryDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryDto>();
            return View(list);
        }




        // GET: /Inventory/WithPolicy/{id}
        [HttpGet]
        public async Task<IActionResult> WithPolicy(Guid id)
        {
            var apiResponse = await _inventoryService.GetWithPolicyAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب بيانات مع تفاصيل السياسة.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryWithPolicyDto>(apiResponse.Result.ToString());
            return View(dto);
        }

        // GET: /Inventory/WithTransactions/{id}
        [HttpGet]
        public async Task<IActionResult> WithTransactions(Guid id)
        {
            var apiResponse = await _inventoryService.GetWithTransactionsAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب بيانات مع تفاصيل الحركات.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryWithTransactionsDto>(apiResponse.Result.ToString());
            return View(dto);
        }

        // GET: /Inventory/WithNotifications/{id}
        [HttpGet]
        public async Task<IActionResult> WithNotifications(Guid id)
        {
            var apiResponse = await _inventoryService.GetWithNotificationsAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب بيانات مع التنبيهات.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryWithNotificationsDto>(apiResponse.Result.ToString());
            return View(dto);
        }

        // GET: /Inventory/Transactions/{id}
        [HttpGet]
        public async Task<IActionResult> Transactions(Guid id)
        {
            var apiResponse = await _inventoryService.GetTransactionsAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب الحركات.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryTransactionDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryTransactionDto>();
            return View(list);
        }

        // GET: /Inventory/Policy/{id}
        [HttpGet]
        public async Task<IActionResult> Policy(Guid id)
        {
            var apiResponse = await _inventoryService.GetPolicyAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب السياسة.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryPolicyDto>(apiResponse.Result.ToString());
            return View(dto);
        }

        // GET: /Inventory/Count
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            var apiResponse = await _inventoryService.GetCountAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب عدد السجلات.";
                return RedirectToAction(nameof(Index));
            }

            var count = JsonConvert
                .DeserializeObject<InventoryCountDto>(apiResponse.Result.ToString())
                ?? new InventoryCountDto();
            return View(count);
        }

        // GET: /Inventory/CountByProduct/{productId}
        [HttpGet]
        public async Task<IActionResult> CountByProduct(Guid productId)
        {
            var apiResponse = await _inventoryService.GetCountByProductAsync(productId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب العدد حسب المنتج.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryCountDto>(apiResponse.Result.ToString())
                ?? new InventoryCountDto();
            return View(dto);
        }

        // GET: /Inventory/CountByWarehouse/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> CountByWarehouse(Guid warehouseId)
        {
            var apiResponse = await _inventoryService.GetCountByWarehouseAsync(warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب العدد حسب المستودع.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryCountDto>(apiResponse.Result.ToString())
                ?? new InventoryCountDto();
            return View(dto);
        }

        // GET: /Inventory/LowStockCount/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> LowStockCount(Guid warehouseId)
        {
            var apiResponse = await _inventoryService.GetLowStockCountAsync(warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب عدد العناصر المنخفضة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<WarehouseLowStockCountDto>(apiResponse.Result.ToString())
                ?? new WarehouseLowStockCountDto();
            return View(dto);
        }

        // GET: /Inventory/OverstockCount/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> OverstockCount(Guid warehouseId)
        {
            var apiResponse = await _inventoryService.GetOverstockedCountAsync(warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب عدد العناصر الزائدة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<WarehouseOverstockCountDto>(apiResponse.Result.ToString())
                ?? new WarehouseOverstockCountDto();
            return View(dto);
        }

        // GET: /Inventory/Availability/{productId}
        [HttpGet]
        public async Task<IActionResult> Availability(Guid productId)
        {
            var apiResponse = await _inventoryService.GetAvailabilityAsync(productId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب التوفر.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<ProductAvailabilityDto>(apiResponse.Result.ToString())
                ?? new ProductAvailabilityDto();
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> AverageLevel(Guid warehouseId)
        {
            var apiResponse = await _inventoryService.GetAverageLevelAsync(warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب متوسط المستوى.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<WarehouseAverageStockLevelDto>(apiResponse.Result.ToString())
                ?? new WarehouseAverageStockLevelDto();
            return View(dto);
        }



        // GET: /Inventory/CurrentAfterAdjustments?productId={productId}&warehouseId={warehouseId}
        [HttpGet]
        public async Task<IActionResult> CurrentAfterAdjustments(Guid productId, Guid warehouseId)
        {
            var apiResponse = await _inventoryService.GetCurrentAfterAdjustmentsAsync(productId, warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب الكمية الحالية بعد التعديلات.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryDto>(apiResponse.Result.ToString())
                ?? new InventoryDto();
            return View(dto);
        }

        // GET: /Inventory/BelowReorder
        [HttpGet]
        public async Task<IActionResult> BelowReorder()
        {
            var apiResponse = await _inventoryService.GetBelowReorderAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب العناصر تحت حد إعادة الطلب.";
                return View(Enumerable.Empty<InventoryDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryDto>();
            return View(list);
        }

        // GET: /Inventory/AboveMax
        [HttpGet]
        public async Task<IActionResult> AboveMax()
        {
            var apiResponse = await _inventoryService.GetAboveMaxAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب العناصر فوق الحد الأقصى.";
                return View(Enumerable.Empty<InventoryDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryDto>();
            return View(list);
        }
    }
}
