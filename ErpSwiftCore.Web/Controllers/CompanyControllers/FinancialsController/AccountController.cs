using AutoMapper;
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.IService.IFinancialsService;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.FinancialsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ISelectListService _selectListService;


        public AccountController(IAccountService accountService, ISelectListService selectListService, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _selectListService = selectListService ?? throw new ArgumentNullException(nameof(selectListService));
        }

        // ─────────────── Index ───────────────
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _accountService.GetAllAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب الحسابات.";
                return View(Enumerable.Empty<AccountDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<AccountDto>>(resp.Result.ToString())
                ?? new List<AccountDto>();

            return View(list);
        }

        // ─────────────── Details ───────────────
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var resp = await _accountService.GetByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "الحساب غير موجود.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<AccountDto>(resp.Result.ToString())
                ?? new AccountDto();

            return View(dto);
        }

        // ─────────────── Upsert (Create / Edit) ───────────────
        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            // prepare parents list
            var parentsVm = new AccountViewModel
            {
                Account = new AccountDto(), 
                CurrencyList = await _selectListService.GetCurrencySelectListAsync()
            };

            var parentsResp = await _accountService.GetAllAsync();
            if (parentsResp?.IsSuccess == true)
            {
                var parents = JsonConvert
                    .DeserializeObject<List<AccountDto>>(parentsResp.Result.ToString())
                    ?? new List<AccountDto>();

                 
            }
            else
            {
                
                if (parentsResp != null && !parentsResp.IsSuccess)
                    TempData["error"] = parentsResp.ErrorMessages.FirstOrDefault();
            }
            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _accountService.GetByIdAsync(id.Value);
                if (resp == null || !resp.IsSuccess)
                {
                    TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                       ?? "الحساب غير موجود.";
                    return RedirectToAction(nameof(Index));
                }

                var dto = JsonConvert
                    .DeserializeObject<AccountDto>(resp.Result.ToString())
                    ?? new AccountDto();

                parentsVm.Account = dto;
            }
            else
            {
                parentsVm.Account = new AccountDto();
            }

            return View(parentsVm);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AccountViewModel vm)
        { 
            vm.CurrencyList = await _selectListService.GetCurrencySelectListAsync(vm.Account.CurrencyId);

            // server-side validation
            if (string.IsNullOrWhiteSpace(vm.Account.AccountNumber))
                ModelState.AddModelError(nameof(vm.Account.AccountNumber), "الرجاء إدخال رقم الحساب.");
            if (string.IsNullOrWhiteSpace(vm.Account.Name))
                ModelState.AddModelError(nameof(vm.Account.Name), "الرجاء إدخال اسم الحساب.");

            //if (!ModelState.IsValid)
            //    return View(vm);

            bool isCreate = vm.Account.ID == Guid.Empty;
            APIResponseDto? resp;

            if (isCreate)
            {
                var createDto = _mapper.Map<CreateAccountDto>(vm.Account);
                resp = await _accountService.CreateAsync(createDto);
            }
            else
            {
                var updateDto = _mapper.Map<UpdateAccountDto>(vm.Account);
                resp = await _accountService.UpdateAsync(updateDto);
            }

            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? (isCreate ? "تم إنشاء الحساب بنجاح." : "تم تحديث الحساب بنجاح.")
                    : resp?.ErrorMessages.FirstOrDefault()!;

            // if failure, redisplay with validation/errors
            if (resp == null || !resp.IsSuccess)
                return View(vm);

            return RedirectToAction(nameof(Index));
        }



        // ─────────────── Delete Operations ───────────────
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resp = await _accountService.DeleteAsync(id);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف الحساب بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد حسابات للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var resp = await _accountService.DeleteRangeAsync(ids);
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف الحسابات المحددة بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            var resp = await _accountService.DeleteAllAsync();
            TempData[resp?.IsSuccess == true ? "success" : "error"] =
                resp?.IsSuccess == true
                    ? "تم حذف جميع الحسابات بنجاح."
                    : resp?.ErrorMessages.FirstOrDefault()!;
            return RedirectToAction(nameof(Index));
        }

        // ─────────────── JSON Endpoints ───────────────
        [HttpGet]
        public async Task<IActionResult> ByParent(Guid parentId)
        {
            var resp = await _accountService.GetByParentAsync(parentId);
            if (resp == null || !resp.IsSuccess)
                return BadRequest(resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب الحسابات الفرعية.");

            var list = JsonConvert
                .DeserializeObject<List<AccountDto>>(resp.Result.ToString())
                ?? new List<AccountDto>();

            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> ByTransactionType(TransactionType type)
        {
            var resp = await _accountService.GetByTransactionTypeAsync(type);
            if (resp == null || !resp.IsSuccess)
                return BadRequest(resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب الحسابات حسب نوع العملية.");

            var list = JsonConvert
                .DeserializeObject<List<AccountDto>>(resp.Result.ToString())
                ?? new List<AccountDto>();

            return Json(list);
        }

        [HttpGet]
        public async Task<IActionResult> Hierarchy(Guid rootId)
        {
            var resp = await _accountService.GetHierarchyAsync(rootId);
            if (resp == null || !resp.IsSuccess)
                return BadRequest(resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب هيكل الحساب.");

            var tree = JsonConvert
                .DeserializeObject<List<AccountDto>>(resp.Result.ToString())
                ?? new List<AccountDto>();

            return Json(tree);
        }

        [HttpGet]
        public async Task<IActionResult> WithParent(Guid id)
        {
            var resp = await _accountService.GetWithParentAsync(id);
            if (resp == null || !resp.IsSuccess)
                return BadRequest(resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب الحساب مع الأصل.");

            var dto = JsonConvert
                .DeserializeObject<AccountDto>(resp.Result.ToString())
                ?? new AccountDto();

            return Json(dto);
        }

        [HttpGet]
        public async Task<IActionResult> TotalBalanceByType(TransactionType type)
        {
            var resp = await _accountService.GetTotalBalanceByTypeAsync(type);
            if (resp == null || !resp.IsSuccess)
                return BadRequest(resp?.ErrorMessages.FirstOrDefault() ?? "فشل جلب إجمالي الرصيد.");

            var total = JsonConvert
                .DeserializeObject<decimal>(resp.Result.ToString());

            return Json(total);
        }
    }
}
