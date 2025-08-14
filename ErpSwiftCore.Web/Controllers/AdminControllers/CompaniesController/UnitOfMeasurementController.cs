using AutoMapper;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements;
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
    public class UnitOfMeasurementController : Controller
    {
        private readonly IUnitOfMeasurementService _unitService;
        private readonly IMapper _mapper;

        public UnitOfMeasurementController(
            IUnitOfMeasurementService unitService,
            IMapper mapper)
        {
            _unitService = unitService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var apiResponse = await _unitService.GetAllUnitsOfMeasurementAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                    ?? "حدث خطأ أثناء جلب قائمة الوحدات.";
                return View(Enumerable.Empty<UnitOfMeasurementDto>());
            }

            var units = JsonConvert
                .DeserializeObject<List<UnitOfMeasurementDto>>(apiResponse.Result.ToString())
                ?? new List<UnitOfMeasurementDto>();

            return View(units);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var apiResponse = await _unitService.GetUnitOfMeasurementByIdAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                    ?? "لم يتم العثور على بيانات الوحدة.";
                return RedirectToAction(nameof(Index));
            }

            var model = JsonConvert
                .DeserializeObject<UnitOfMeasurementDto>(apiResponse.Result.ToString());

            if (model == null)
            {
                TempData["error"] = "تعذر فك بيانات الوحدة.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            // Prepare an empty DTO for create
            var dto = new UnitOfMeasurementDto();

            if (id == null || id == Guid.Empty)
            {
                // Create mode
                return View(dto);
            }

            // Edit mode: fetch existing
            var apiResponse = await _unitService.GetUnitOfMeasurementByIdAsync(id.Value);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                    ?? "لم يتم العثور على بيانات الوحدة.";
                return RedirectToAction(nameof(Index));
            }

            dto = JsonConvert
                .DeserializeObject<UnitOfMeasurementDto>(apiResponse.Result.ToString())
                  ?? new UnitOfMeasurementDto();

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(UnitOfMeasurementDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ID == Guid.Empty)
            {
                // Create new unit
                var createDto = _mapper.Map<UnitOfMeasurementCreateDto>(model);
                var apiResponse = await _unitService.CreateUnitOfMeasurementAsync(createDto);

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء الوحدة.");
                    return View(model);
                }

                TempData["success"] = "تم إنشاء الوحدة بنجاح.";
            }
            else
            {
                // Update existing unit
                var updateDto = _mapper.Map<UnitOfMeasurementUpdateDto>(model);
                var apiResponse = await _unitService.UpdateUnitOfMeasurementAsync(updateDto);

                if (apiResponse == null || !apiResponse.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty,
                        apiResponse?.ErrorMessages.FirstOrDefault() ?? "فشل تحديث بيانات الوحدة.");
                    return View(model);
                }

                TempData["success"] = "تم تحديث بيانات الوحدة بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiResponse = await _unitService.DeleteUnitOfMeasurementAsync(id);
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                    ?? "فشل حذف الوحدة.";
            }
            else
            {
                TempData["success"] = "تم حذف الوحدة بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var apiResponse = await _unitService.DeleteAllUnitsOfMeasurementAsync();
            if (apiResponse == null || !apiResponse.IsSuccess)
            {
                TempData["error"] = apiResponse?.ErrorMessages.FirstOrDefault()
                                    ?? "فشل حذف جميع الوحدات.";
            }
            else
            {
                TempData["success"] = "تم حذف جميع الوحدات بنجاح.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
