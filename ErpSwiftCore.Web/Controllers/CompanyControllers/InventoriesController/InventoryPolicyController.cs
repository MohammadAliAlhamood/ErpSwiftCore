using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.InventoryPolicyModels;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.IService.IInventoriesService;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.InventoriesController
{
    [Authorize(Roles = SD.Role_Company)]
    public class InventoryPolicyController : Controller
    {
        private readonly IInventoryPolicyService _policyService;

        public InventoryPolicyController(IInventoryPolicyService policyService)
        {
            _policyService = policyService
                ?? throw new ArgumentNullException(nameof(policyService));
        }

        // GET: /InventoryPolicy
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _policyService.GetAllAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب جميع السياسات.";
                return View(Enumerable.Empty<InventoryPolicyDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryPolicyDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryPolicyDto>();
            return View(list);
        }

        // GET: /InventoryPolicy/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _policyService.GetByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على السياسة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryPolicyDto>(apiResponse.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات السياسة.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        
        // GET: /InventoryPolicy/ByInventory/{inventoryId}
        [HttpGet]
        public async Task<IActionResult> ByInventory(Guid inventoryId)
        {
            var apiResponse = await _policyService.GetByInventoryAsync(inventoryId);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب السياسة للمخزون المحدد.";
                return View(Enumerable.Empty<InventoryPolicyDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryPolicyDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryPolicyDto>();
            return View(list);
        }

        // GET: /InventoryPolicy/ByType/{type}
        [HttpGet]
        public async Task<IActionResult> ByType(InventoryPolicyType type)
        {
            var apiResponse = await _policyService.GetByTypeAsync(type);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب السياسات من النوع المحدد.";
                return View(Enumerable.Empty<InventoryPolicyDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryPolicyDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryPolicyDto>();
            return View(list);
        }

        // GET: /InventoryPolicy/AutoReorder
        [HttpGet]
        public async Task<IActionResult> AutoReorder()
        {
            var apiResponse = await _policyService.GetWithAutoReorderAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب السياسات ذات إعادة الطلب التلقائي.";
                return View(Enumerable.Empty<InventoryPolicyDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryPolicyDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryPolicyDto>();
            return View("ByType", list);
        }

        // GET: /InventoryPolicy/BelowReorder
        [HttpGet]
        public async Task<IActionResult> BelowReorder()
        {
            var apiResponse = await _policyService.GetBelowReorderAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب السياسات تحت مستوى إعادة الطلب.";
                return View(Enumerable.Empty<InventoryPolicyDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryPolicyDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryPolicyDto>();
            return View(list);
        }

        // GET: /InventoryPolicy/AboveMax
        [HttpGet]
        public async Task<IActionResult> AboveMax()
        {
            var apiResponse = await _policyService.GetAboveMaxAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل جلب السياسات فوق الحد الأقصى.";
                return View(Enumerable.Empty<InventoryPolicyDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<InventoryPolicyDto>>(apiResponse.Result.ToString())
                ?? new List<InventoryPolicyDto>();
            return View(list);
        }
 




        // POST: /InventoryPolicy/EnableAutoReorder
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EnableAutoReorder(EnableAutoReorderDto dto)
        {
            var apiResponse = await _policyService.EnableAutoReorderAsync(dto);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم تفعيل إعادة الطلب التلقائي بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تفعيل إعادة الطلب التلقائي.";
            return RedirectToAction(nameof(Index));
        }
        // POST: /InventoryPolicy/DisableAutoReorder
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DisableAutoReorder(DisableAutoReorderDto dto)
        {
            var apiResponse = await _policyService.DisableAutoReorderAsync(dto);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم إيقاف إعادة الطلب التلقائي بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل إيقاف إعادة الطلب التلقائي.";
            return RedirectToAction(nameof(Index));
        }




        // GET: /InventoryPolicy/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var apiResponse = await _policyService.GetByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على السياسة.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<InventoryPolicyDto>(apiResponse.Result.ToString())
                ?? new InventoryPolicyDto();
            var vm = new UpdatePolicyDto
            {
                ID = dto.ID,
                ReorderLevel = dto.ReorderLevel,
                MaxStockLevel = dto.MaxStockLevel,
                PolicyType = dto.PolicyType
            };
            return View(vm);
        }

        // POST: /InventoryPolicy/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdatePolicyDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var apiResponse = await _policyService.UpdatePolicyAsync(dto);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                ModelState.AddModelError(string.Empty,
                    apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث السياسة.");
                return View(dto);
            }

            TempData["success"] = "تم تحديث السياسة بنجاح.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /InventoryPolicy/UpdateReorderLevel
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReorderLevel(UpdateReorderLevelDto dto)
        {
            var apiResponse = await _policyService.UpdateReorderLevelAsync(dto);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم تحديث حد إعادة الطلب بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث حد إعادة الطلب.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /InventoryPolicy/UpdateMaxStockLevel
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMaxStockLevel(UpdateMaxStockLevelDto dto)
        {
            var apiResponse = await _policyService.UpdateMaxStockLevelAsync(dto);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم تحديث الحد الأقصى للمخزون بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث الحد الأقصى للمخزون.";
            return RedirectToAction(nameof(Index));
        }

        // POST: /InventoryPolicy/UpdateRange
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRange(UpdatePoliciesRangeDto dto)
        {
            var apiResponse = await _policyService.UpdateRangeAsync(dto);
            TempData[apiResponse?.IsSuccess == true ? "success" : "error"] =
                apiResponse?.IsSuccess == true
                    ? "تم تحديث مجموعة السياسات بنجاح."
                    : apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث مجموعة السياسات.";
            return RedirectToAction(nameof(Index));
        }
    }
}
