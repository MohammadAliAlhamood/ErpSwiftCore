using AutoMapper;
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.BillingsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ISelectListService _selectListService;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderService orderService,
            ISelectListService selectListService,
            IMapper mapper)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _selectListService = selectListService ?? throw new ArgumentNullException(nameof(selectListService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resp = await _orderService.GetAllWithDetailsAsync();
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "حدث خطأ أثناء جلب قائمة الطلبات.";
                return View(Enumerable.Empty<OrderDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<OrderDto>>(resp.Result.ToString())
                ?? new List<OrderDto>();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> ByStatus(OrderStatus status)
        {
            var resp = await _orderService.GetByStatusAsync(status);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? $"حدث خطأ أثناء جلب الطلبات بحالة {status}.";
                return View("Index", Enumerable.Empty<OrderDto>());
            }

            var list = JsonConvert
                .DeserializeObject<List<OrderDto>>(resp.Result.ToString())
                ?? new List<OrderDto>();

            // نعيد استخدام نفس الـ View الخاص بالـ Index لكن مع البيانات المفلترة
            ViewData["FilterStatus"] = status;
            return View("Index", list);
        }


        // ─────────────── Details ───────────────
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            APIResponseDto resp = await _orderService.GetByIdAsync(id);
            if (resp == null || !resp.IsSuccess)
            {
                TempData["error"] = resp?.ErrorMessages.FirstOrDefault()
                                   ?? "لم يتم العثور على الطلب المطلوب.";
                return RedirectToAction(nameof(Index));
            }

            var dto = JsonConvert
                .DeserializeObject<OrderDto>(resp.Result.ToString());
            if (dto == null)
            {
                TempData["error"] = "تعذّر فك بيانات الطلب.";
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }





        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            var vm = new OrderViewModel
            {
                Order = new OrderDto
                {
                    OrderDate = DateTime.Today
                }
            };

            await PopulateSelectListsAsync(vm);

            if (id.HasValue && id.Value != Guid.Empty)
            {
                var resp = await _orderService.GetByIdAsync(id.Value);
                if (resp?.IsSuccess == true && resp.Result != null)
                {
                    // خريطة DTO → ViewModel عبر AutoMapper
                    vm.Order = _mapper.Map<OrderDto>(JsonConvert.DeserializeObject(resp.Result.ToString())!);

                    // من جديد: تخصيص القوائم بالقيمة المحددة
                    await PopulateSelectListsAsync(vm, vm.Order.Party.Type, vm.Order.PartyId, vm.Order.CurrencyId);
                }
                else
                {
                    TempData["Error"] = "تعذّر العثور على الطلب المحدد.";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(OrderViewModel vm)
        {
            // إعادة تعبئة القوائم دائمًا
            await PopulateSelectListsAsync(vm, vm.Order.Party.Type, vm.Order.PartyId, vm.Order.CurrencyId);

   
            try
            {
                APIResponseDto apiResp;
                bool isCreate = vm.Order.ID == Guid.Empty;

                if (isCreate)
                {
                    // تحويل OrderDto → CreateOrderDto
                    var createOrder = _mapper.Map<CreateOrderDto>(vm.Order);
                    // تحويل OrderLineDto → CreateOrderLineDto
                    var createLines = _mapper.Map<IEnumerable<CreateOrderLineDto>>(vm.Order.OrderLines);

                    var createDto = new CreateOrderWithLinesDto
                    {
                        Order = createOrder,
                        OrderLines = createLines
                    };

                    apiResp = await _orderService.CreateWithLinesAsync(createDto);
                    TempData[apiResp.IsSuccess ? "Success" : "Error"] =
                        apiResp.IsSuccess
                            ? "تم إنشاء الطلب مع سطوره بنجاح."
                            : apiResp.ErrorMessages.FirstOrDefault() ?? "فشل إنشاء الطلب.";
                }
                else
                {
                    // تحويل OrderDto → UpdateOrderDto
                    var updateOrder = _mapper.Map<UpdateOrderDto>(vm.Order);
                    // تقسيم السطور إلى إضافات وتحديثات
                    var linesToAdd = vm.Order.OrderLines
                        .Where(l => l.ID == Guid.Empty)
                        .Select(l => _mapper.Map<UpdateOrderLineDto>(l));
                    var linesToUpdate = vm.Order.OrderLines
                        .Where(l => l.ID != Guid.Empty)
                        .Select(l => _mapper.Map<UpdateOrderLineDto>(l));

                    var updateDto = new UpdateOrderWithLinesDto
                    {
                        Order = updateOrder,
                        LinesToAdd = linesToAdd,
                        LinesToUpdate = linesToUpdate,
                        LineIdsToDelete = vm.DeletedLineIds
                    };

                    apiResp = await _orderService.UpdateWithLinesAsync(updateDto);
                    TempData[apiResp.IsSuccess ? "Success" : "Error"] =
                        apiResp.IsSuccess
                            ? "تم تحديث الطلب وسطوره بنجاح."
                            : apiResp.ErrorMessages.FirstOrDefault() ?? "فشل تحديث الطلب.";
                }

                if (!apiResp.IsSuccess)
                    return View(vm);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // هنا يمكنك تسجيل الخطأ باستخدام ILogger إن كنت تستخدمه
                ModelState.AddModelError(string.Empty, "حدث خطأ غير متوقع، الرجاء المحاولة لاحقًا.");
                return View(vm);
            }
        }

        /// <summary>
        /// يجهّز كل قوائم الاختيار دفعة واحدة، مع إمكانية تحديد العنصر المختار لكل قائمة
        /// </summary>
        private async Task PopulateSelectListsAsync(OrderViewModel vm, PartyType? partyType = null, Guid? selectedPartyId = null, Guid? selectedCurrencyId = null)
        {
            // العملات
            vm.CurrencyList = await _selectListService
                .GetCurrencySelectListAsync(selectedCurrencyId);

            // العملاء والموردين بحسب نوع الطرف
            vm.CustomerList = partyType == PartyType.Customer
                ? await _selectListService.GetCustomerSelectListAsync(selectedPartyId)
                : await _selectListService.GetCustomerSelectListAsync(null);

            vm.SupplierList = partyType == PartyType.Supplier
                ? await _selectListService.GetSupplierSelectListAsync(selectedPartyId)
                : await _selectListService.GetSupplierSelectListAsync(null);

            // المنتجات
            // في حال كانت قائمة المنتجات كبيرة: يمكن تطبيق pagination أو تحميلها عبر AJAX لاحقًا
            vm.ProductList = await _selectListService.GetProductSelectListAsync();
        }





        // ─────────────── Delete Single Order ───────────────
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var apiResp = await _orderService.DeleteAsync(id);

            TempData[apiResp?.IsSuccess == true ? "success" : "error"] =
                apiResp?.IsSuccess == true
                    ? "تم حذف الطلب بنجاح."
                    : apiResp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف الطلب.";

            return RedirectToAction(nameof(Index));
        }

        // ─────────────── Delete Multiple Orders ───────────────
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                TempData["error"] = "لم يتم تحديد أي طلب للحذف.";
                return RedirectToAction(nameof(Index));
            }

            var apiResp = await _orderService.BulkDeleteAsync(ids);

            TempData[apiResp?.IsSuccess == true ? "success" : "error"] =
                apiResp?.IsSuccess == true
                    ? "تم حذف مجموعة الطلبات بنجاح."
                    : apiResp?.ErrorMessages.FirstOrDefault() ?? "فشل حذف مجموعة الطلبات.";

            return RedirectToAction(nameof(Index));
        }
    }
}
