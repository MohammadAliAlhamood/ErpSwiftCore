using AutoMapper;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Controllers.CompaniesController
{
    [Authorize(Roles = SD.Role_Admin)]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;
        private readonly IMapper _mapper;

        public CurrencyController(
            ICurrencyService currencyService,
            IMapper mapper)
        {
            _currencyService = currencyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _currencyService.GetAllCurrenciesAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب قائمة العملات.";
                return View(Enumerable.Empty<CurrencyDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<CurrencyDto>>(apiResponse.Result.ToString())
                ?? new List<CurrencyDto>();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _currencyService.GetCurrencyByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على بيانات العملة.";
                return RedirectToAction(nameof(Index));
            }

            var model = JsonConvert
                .DeserializeObject<CurrencyDto>(apiResponse.Result.ToString());

            if (model == null)
            {
                TempData["error"] = "تعذر فك بيانات العملة.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            // for create
            var vm = new CurrencyDto();

            if (id == null || id == Guid.Empty)
            {
                return View(vm);
            }

            // for edit
            var apiResponse = await _currencyService.GetCurrencyByIdAsync(id.Value);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على بيانات العملة.";
                return RedirectToAction(nameof(Index));
            }

            vm = JsonConvert
                .DeserializeObject<CurrencyDto>(apiResponse.Result.ToString())
                ?? new CurrencyDto();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CurrencyDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ID == Guid.Empty)
            {
                // Create
                var createDto = _mapper.Map<CurrencyCreateDto>(model);
                var apiResponse = await _currencyService.CreateCurrencyAsync(createDto);

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء العملة.");
                    return View(model);
                }

                TempData["success"] = "تم إنشاء العملة بنجاح.";
            }
            else
            {
                // Update
                var updateDto = _mapper.Map<CurrencyUpdateDto>(model);
                var apiResponse = await _currencyService.UpdateCurrencyAsync(updateDto);

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث بيانات العملة.");
                    return View(model);
                }

                TempData["success"] = "تم تحديث بيانات العملة بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiResponse = await _currencyService.DeleteCurrencyAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل حذف العملة.";
            }
            else
            {
                TempData["success"] = "تم حذف العملة بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var apiResponse = await _currencyService.DeleteAllCurrenciesAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                   ?? "فشل حذف جميع العملات.";
            }
            else
            {
                TempData["success"] = "تم حذف جميع العملات بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
