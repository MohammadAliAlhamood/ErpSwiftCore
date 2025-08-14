using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryModels;
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryTransactionModels;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.IService.IInventoriesService;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.InventoriesController
{
    [Authorize(Roles = SD.Role_Company)]
    public class InventoryTransactionController : Controller
    {
        private readonly IInventoryTransactionService _transactionService;

        public InventoryTransactionController(IInventoryTransactionService transactionService)
        {
            _transactionService = transactionService
                ?? throw new ArgumentNullException(nameof(transactionService));
        }

        // GET: /InventoryTransaction
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _transactionService.GetCountAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب عدد الحركات.";
                return View("Count", Enumerable.Empty<InventoryCountDto>());
            }

            var countDto = JsonConvert
                .DeserializeObject<InventoryCountDto>(apiResponse.Result.ToString())
                ?? new InventoryCountDto();
            return View("Count", countDto);
        }

        // GET: /InventoryTransaction/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _transactionService.GetByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على الحركة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryTransactionDto>(apiResponse.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات الحركة.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        // GET: /InventoryTransaction/FirstForInventory/{inventoryId}
        [HttpGet]
        public async Task<IActionResult> FirstForInventory(Guid inventoryId)
        {
            var apiResponse = await _transactionService.GetFirstForInventoryAsync(inventoryId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب أول حركة للمخزون.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryTransactionDto>(apiResponse.Result.ToString())
                ?? new InventoryTransactionDto();
            return View("Details", dto);
        }

        // GET: /InventoryTransaction/LastForInventory/{inventoryId}
        [HttpGet]
        public async Task<IActionResult> LastForInventory(Guid inventoryId)
        {
            var apiResponse = await _transactionService.GetLastForInventoryAsync(inventoryId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب آخر حركة للمخزون.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryTransactionDto>(apiResponse.Result.ToString())
                ?? new InventoryTransactionDto();
            return View("Details", dto);
        }

        // GET: /InventoryTransaction/ByInventory/{inventoryId}
        [HttpGet]
        public async Task<IActionResult> ByInventory(Guid inventoryId)
        {
            var apiResponse = await _transactionService.GetByInventoryAsync(inventoryId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب حركات المخزون.";
                return View(Enumerable.Empty<InventoryTransactionDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryTransactionDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryTransactionDto>();
            return View(list);
        }

        // GET: /InventoryTransaction/ByProduct/{productId}
        [HttpGet]
        public async Task<IActionResult> ByProduct(Guid productId)
        {
            var apiResponse = await _transactionService.GetByProductAsync(productId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب حركات المنتج.";
                return View(Enumerable.Empty<InventoryTransactionDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryTransactionDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryTransactionDto>();
            return View(list);
        }

        // GET: /InventoryTransaction/ByWarehouse/{warehouseId}
        [HttpGet]
        public async Task<IActionResult> ByWarehouse(Guid warehouseId)
        {
            var apiResponse = await _transactionService.GetByWarehouseAsync(warehouseId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب حركات المستودع.";
                return View(Enumerable.Empty<InventoryTransactionDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryTransactionDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryTransactionDto>();
            return View(list);
        }

        // GET: /InventoryTransaction/ByType/{type}
        [HttpGet]
        public async Task<IActionResult> ByType(InventoryTransactionType type)
        {
            var apiResponse = await _transactionService.GetByTypeAsync(type);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب الحركات من النوع المحدد.";
                return View(Enumerable.Empty<InventoryTransactionDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryTransactionDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryTransactionDto>();
            return View(list);
        }

        // GET: /InventoryTransaction/ByDateRange?from={from}&to={to}
        [HttpGet]
        public async Task<IActionResult> ByDateRange(DateTime from, DateTime to)
        {
            var apiResponse = await _transactionService.GetByDateRangeAsync(from, to);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب الحركات ضمن النطاق الزمني.";
                return View(Enumerable.Empty<InventoryTransactionDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryTransactionDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryTransactionDto>();
            return View(list);
        }

        // GET: /InventoryTransaction/AffectingBalance/{inventoryId}
        [HttpGet]
        public async Task<IActionResult> AffectingBalance(Guid inventoryId)
        {
            var apiResponse = await _transactionService.GetAffectingBalanceAsync(inventoryId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب الحركات المؤثرة في الرصيد.";
                return View(Enumerable.Empty<InventoryTransactionDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryTransactionDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryTransactionDto>();
            return View(list);
        }

        // GET: /InventoryTransaction/SearchByNotes?term={noteTerm}
        [HttpGet]
        public async Task<IActionResult> SearchByNotes(string term)
        {
            var apiResponse = await _transactionService.SearchByNotesAsync(term);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل البحث في الملاحظات.";
                return View(Enumerable.Empty<InventoryTransactionDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryTransactionDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryTransactionDto>();
            return View(list);
        }

        // GET: /InventoryTransaction/Count
        [HttpGet]
        public async Task<IActionResult> Count()
        {
            var apiResponse = await _transactionService.GetCountAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب العدد.";
                return RedirectToAction(nameof(Index));
            }

            var countDto = JsonConvert
                .DeserializeObject<InventoryCountDto>(apiResponse.Result.ToString())
                ?? new InventoryCountDto();
            return View(countDto);
        }

        // GET: /InventoryTransaction/SumQuantity?productId={productId}&from={from}&to={to}
        
    }
}
