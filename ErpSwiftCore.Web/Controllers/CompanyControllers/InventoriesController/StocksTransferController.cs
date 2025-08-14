using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.StocksTransferModels;
using ErpSwiftCore.Web.Helpers;      
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.IService.IInventoriesService; // for warehouse select list

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.InventoriesController
{
    [Authorize(Roles = SD.Role_Company)]
    public class StocksTransferController : Controller
    {
        private readonly IStocksTransferService _transferService;
        private readonly ISelectListService _SelectList; 

        public StocksTransferController(
            IStocksTransferService transferService,
            ISelectListService productSelectList )
        {
            _transferService = transferService ?? throw new ArgumentNullException(nameof(transferService));
            _SelectList = productSelectList ?? throw new ArgumentNullException(nameof(productSelectList));
        }

        // GET: /StocksTransfer
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _transferService.GetAllAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب جميع عمليات النقل.";
                return View(Enumerable.Empty<StockTransferDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<StockTransferDto>>(resp.Result.ToString())
                ?? new List<StockTransferDto>();
            return View(list);
        }

        // GET & POST: Create or Edit
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new StockTransferViewModel
            {
                Transfer = new StockTransferDto { TransferDate = DateTime.Now },
                ProductList = await _SelectList.GetProductSelectListAsync(),
                FromWarehouseList = await _SelectList.GetWarehouseSelectListAsync(),
                ToWarehouseList = await _SelectList.GetWarehouseSelectListAsync()
            };

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _transferService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على عملية النقل.";
                    return RedirectToAction(nameof(Index));
                }

                var dto = JsonConvert
                    .DeserializeObject<StockTransferDto>(resp.Result.ToString())
                    ?? new StockTransferDto();

                vm.Transfer = dto;
                vm.ProductList = await _SelectList.GetProductSelectListAsync(dto.ProductID);
                vm.FromWarehouseList = await _SelectList.GetWarehouseSelectListAsync(dto.FromWarehouseID);
                vm.ToWarehouseList = await _SelectList.GetWarehouseSelectListAsync(dto.ToWarehouseID);
            }

            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(StockTransferViewModel vm)
        {
            // repopulate selects
            vm.ProductList = await _SelectList.GetProductSelectListAsync(vm.Transfer.ProductID);
            vm.FromWarehouseList = await _SelectList.GetWarehouseSelectListAsync(vm.Transfer.FromWarehouseID);
            vm.ToWarehouseList = await _SelectList.GetWarehouseSelectListAsync(vm.Transfer.ToWarehouseID);

            // validation
            if (vm.Transfer.ProductID == Guid.Empty)
                ModelState.AddModelError(nameof(vm.Transfer.ProductID), "الرجاء اختيار منتج.");
            if (vm.Transfer.FromWarehouseID == Guid.Empty)
                ModelState.AddModelError(nameof(vm.Transfer.FromWarehouseID), "الرجاء اختيار مستودع الإرسال.");
            if (vm.Transfer.ToWarehouseID == Guid.Empty)
                ModelState.AddModelError(nameof(vm.Transfer.ToWarehouseID), "الرجاء اختيار مستودع الاستلام.");
            if (vm.Transfer.FromWarehouseID == vm.Transfer.ToWarehouseID)
                ModelState.AddModelError(string.Empty, "لا يمكن أن يكون المستودع المرسل والمستودع المستلم متماثلين.");
            if (vm.Transfer.Quantity <= 0)
                ModelState.AddModelError(nameof(vm.Transfer.Quantity), "يجب أن تكون الكمية أكبر من صفر.");

            if (!ModelState.IsValid)
                return View(vm);

            APIResponseDto? resp;

            if (vm.Transfer.ID == Guid.Empty)
            {
                var createDto = new CreateStockTransferDto
                {
                    ProductID = vm.Transfer.ProductID,
                    FromWarehouseID = vm.Transfer.FromWarehouseID,
                    ToWarehouseID = vm.Transfer.ToWarehouseID,
                    Quantity = vm.Transfer.Quantity,
                    TransferDate = vm.Transfer.TransferDate,
                    ReferenceNumber = vm.Transfer.ReferenceNumber,
                    Notes = vm.Transfer.Notes
                };
                resp = await _transferService.CreateAsync(createDto);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء عملية النقل.";
                    return View(vm);
                }
                TempData["success"] = "تم إنشاء عملية النقل بنجاح.";
            }
            else
            {
                var updateDto = new UpdateStockTransferDto
                {
                    ID = vm.Transfer.ID,
                    ProductID = vm.Transfer.ProductID,
                    FromWarehouseID = vm.Transfer.FromWarehouseID,
                    ToWarehouseID = vm.Transfer.ToWarehouseID,
                    Quantity = vm.Transfer.Quantity,
                    TransferDate = vm.Transfer.TransferDate,
                    ReferenceNumber = vm.Transfer.ReferenceNumber,
                    Notes = vm.Transfer.Notes
                };
                resp = await _transferService.UpdateAsync(updateDto);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث عملية النقل.";
                    return View(vm);
                }
                TempData["success"] = "تم تحديث عملية النقل بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /StocksTransfer/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var resp = await _transferService.GetByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault() ?? "لم يتم العثور على عملية النقل.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<StockTransferDto>(resp.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات النقل.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        // POST: /StocksTransfer/Delete/{id}
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _transferService.DeleteAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف عملية النقل بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف عملية النقل.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /StocksTransfer/DeleteRange
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي عمليات للنقل للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _transferService.DeleteRangeAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف مجموعة عمليات النقل بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة عمليات النقل.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /StocksTransfer/DeleteAll
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var resp = await _transferService.DeleteAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف جميع عمليات النقل بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف جميع عمليات النقل.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /StocksTransfer/Restore/{id}
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            var resp = await _transferService.RestoreAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم استرجاع عملية النقل بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل استرجاع عملية النقل.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /StocksTransfer/RestoreRange
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreRange(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي عمليات للنقل للاسترجاع.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _transferService.RestoreRangeAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم استرجاع مجموعة عمليات النقل بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل استرجاع مجموعة عمليات النقل.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /StocksTransfer/RestoreAll
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreAll()
        {
            var resp = await _transferService.RestoreAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم استرجاع جميع عمليات النقل بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault() ?? "فشل استرجاع جميع عمليات النقل.";
            return RedirectToAction(nameof(Index));
        }

        // Additional query examples follow the same pattern...
    }
}
