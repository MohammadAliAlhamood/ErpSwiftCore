using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ErpSwiftCore.Web.IService.IFinancialsService;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.JournalEntryModels;
using ErpSwiftCore.Web.Utility;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.FinancialsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class JournalEntryController : Controller
    {
        private readonly IJournalEntryService _svc;

        public JournalEntryController(IJournalEntryService svc)
        {
            _svc = svc ?? throw new ArgumentNullException(nameof(svc));
        }

        // ─────────────── Index ───────────────
        // عرض القيود في نطاق زمني اختياري (افتراضي آخر 30 يومًا)
        [HttpGet]
        public async Task<IActionResult> Index(DateTime? from, DateTime? to)
        {
            var start = from ?? DateTime.UtcNow.AddDays(-30);
            var end = to ?? DateTime.UtcNow;

            var resp = await _svc.GetByDateRangeAsync(start, end);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب قيود اليومية.";
                return View(Enumerable.Empty<JournalEntryDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<JournalEntryDto>>(resp.Result.ToString())
                ?? new List<JournalEntryDto>();

            return View(list);
        }

        // ─────────────── Details ───────────────
        // عرض القيد مع الأسطر
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var resp = await _svc.GetByIdAsync(id, includeLines: true);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على قيد اليومية.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<JournalEntryDto>(resp.Result.ToString())
                ?? new JournalEntryDto();

            return View(dto);
        }

        // ─────────────── Delete / Restore ───────────────
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _svc.DeleteAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف القيد بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(Guid id)
        {
            var resp = await _svc.RestoreAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم استعادة القيد بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد قيود للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _svc.DeleteRangeAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف القيود المحددة بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            return RedirectToAction(nameof(Index));
        }

        // ─────────────── JSON Endpoints ───────────────
        // جلب أسطر القيد بصيغة JSON
        [HttpGet]
        public async Task<IActionResult> Lines(Guid journalEntryId)
        {
            var resp = await _svc.GetLinesAsync(journalEntryId);
            if (resp == null || !resp.IsSuccess)
                return BadRequest(resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب أسطر القيد.");

            var lines = JsonConvert
                .DeserializeObject<List<JournalEntryLineDto>>(resp.Result.ToString())
                ?? new List<JournalEntryLineDto>();

            return Json(lines);
        }
    }
}
