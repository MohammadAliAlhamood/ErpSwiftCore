using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.IService.ICRMsService;
using ErpSwiftCore.Web.IService.IFinancialsService;
using ErpSwiftCore.Web.IService.IInventoriesService;
using ErpSwiftCore.Web.IService.IProductsService;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.OrderModels;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompanyBranchs;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements;
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.CustomerModels;
using ErpSwiftCore.Web.Models.CRMSystemManagmentModels.SupplierModels;
using ErpSwiftCore.Web.Models.FinancialSystemManagmentModels.AccountModels;
using ErpSwiftCore.Web.Models.InventorySystemManagmentModels.WarehouseModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels;
using ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ErpSwiftCore.Web.Helpers
{
    public class SelectListService : ISelectListService
    {
        private readonly IProductService _productService;
        private readonly ICurrencyService _currencyService;
        private readonly IUnitOfMeasurementService _unitService;
        private readonly IProductCategoryService _categoryService;
        private readonly IWarehouseService _warehouseService;
        private readonly ICompanyBranchService _branchService;
        private readonly IAccountService _accountService;     // جديد
        private readonly IOrderService _orderService;         // جديد

        // إضافات الحقول في أعلى الصنف:
        private readonly ICustomerService _customerService;  // جديد
        private readonly ISupplierService _supplierService;  // جديد

        public SelectListService(
            IProductService productService,
            ICurrencyService currencyService,
            IUnitOfMeasurementService unitService,
            IProductCategoryService categoryService,
            IWarehouseService warehouseService,
            ICompanyBranchService branchService,
            IAccountService accountService,
            IOrderService orderService,
            ICustomerService customerService,    // جديد
            ISupplierService supplierService)    // جديد
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
            _unitService = unitService ?? throw new ArgumentNullException(nameof(unitService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _warehouseService = warehouseService ?? throw new ArgumentNullException(nameof(warehouseService));
            _branchService = branchService ?? throw new ArgumentNullException(nameof(branchService));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _supplierService = supplierService ?? throw new ArgumentNullException(nameof(supplierService));
        }

        public async Task<IEnumerable<SelectListItem>> GetProductSelectListAsync(Guid? selectedId = null)
        {
            var resp = await _productService.GetAllAsync();
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<ProductDto>>(resp.Result.ToString())
                ?? new List<ProductDto>();

            return list.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.ID.ToString(),
                Selected = selectedId.HasValue && p.ID == selectedId.Value
            });
        }
        public async Task<IEnumerable<SelectListItem>> GetCurrencySelectListAsync(Guid? selectedId = null)
        {
            var resp = await _currencyService.GetAllCurrenciesAsync();
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<CurrencyDto>>(resp.Result.ToString())
                ?? new List<CurrencyDto>();

            return list.Select(c => new SelectListItem
            {
                Text = c.CurrencyName,
                Value = c.ID.ToString(),
                Selected = selectedId.HasValue && c.ID == selectedId.Value
            });
        }
        public async Task<IEnumerable<SelectListItem>> GetUnitSelectListAsync(Guid? selectedId = null)
        {
            var resp = await _unitService.GetAllUnitsOfMeasurementAsync();
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<UnitOfMeasurementDto>>(resp.Result.ToString())
                ?? new List<UnitOfMeasurementDto>();

            return list.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.ID.ToString(),
                Selected = selectedId.HasValue && u.ID == selectedId.Value
            });
        }
        public async Task<IEnumerable<SelectListItem>> GetCategorySelectListAsync(Guid? selectedId = null)
        {
            var resp = await _categoryService.GetAllAsync();
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<ProductCategoryDto>>(resp.Result.ToString())
                ?? new List<ProductCategoryDto>();

            return list.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString(),
                Selected = selectedId.HasValue && c.ID == selectedId.Value
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetWarehouseSelectListAsync(Guid? selectedId = null)
        {
            var resp = await _warehouseService.GetAllAsync();
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<WarehouseDto>>(resp.Result.ToString())
                ?? new List<WarehouseDto>();

            return list.Select(w => new SelectListItem
            {
                Text = w.Name,
                Value = w.ID.ToString(),
                Selected = selectedId.HasValue && w.ID == selectedId.Value
            });
        }


        public async Task<IEnumerable<SelectListItem>> GetBranchSelectListAsync(Guid? selectedId = null)
        {
            var resp = await _branchService.GetAllBranchesAsync();
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<CompanyBranchDto>>(resp.Result.ToString())
                ?? new List<CompanyBranchDto>();

            return list.Select(b => new SelectListItem
            {
                Text = b.BranchName,
                Value = b.Id.ToString(),
                Selected = selectedId.HasValue && b.Id == selectedId.Value
            });
        }

        // ... (الطرق الأخرى كما هي) ...

        //public async Task<IEnumerable<SelectListItem>> GetOrderSelectListAsync(Guid? selectedId = null)
        //{
        //    var resp = await _orderService.();
        //    if (resp?.IsSuccess != true || resp.Result == null)
        //        return Enumerable.Empty<SelectListItem>();

        //    var list = JsonConvert
        //        .DeserializeObject<List<OrderDto>>(resp.Result.ToString())
        //        ?? new List<OrderDto>();

        //    return list.Select(o => new SelectListItem
        //    {
        //        Text = o.OrderNumber,
        //        Value = o.ID.ToString(),
        //        Selected = selectedId.HasValue && o.ID == selectedId.Value
        //    });
        //}

        public async Task<IEnumerable<SelectListItem>> GetParentAccountSelectListAsync(Guid? selectedId = null)
        {
            var resp = await _accountService.GetAllAsync();
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<AccountDto>>(resp.Result.ToString())
                ?? new List<AccountDto>();

            return list.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.ID.ToString(),
                Selected = selectedId.HasValue && a.ID == selectedId.Value
            });
        }

        // امبليمنتاشن GetCustomerSelectListAsync
        public async Task<IEnumerable<SelectListItem>> GetCustomerSelectListAsync(Guid? selectedId = null)
        {
            var resp = await _customerService.GetAllAsync();
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<CustomerDto>>(resp.Result.ToString())
                ?? new List<CustomerDto>();

            return list.Select(c => new SelectListItem
            {
                Text = c.FirstName,
                Value = c.ID.ToString(),
                Selected = selectedId.HasValue && c.ID == selectedId.Value
            });
        }

        // امبليمنتاشن GetSupplierSelectListAsync
        public async Task<IEnumerable<SelectListItem>> GetSupplierSelectListAsync(Guid? selectedId = null)
        {
            var resp = await _supplierService.GetAllAsync();
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<SupplierDto>>(resp.Result.ToString())
                ?? new List<SupplierDto>();

            return list.Select(s => new SelectListItem
            {
                Text = s.FirstName,
                Value = s.ID.ToString(),
                Selected = selectedId.HasValue && s.ID == selectedId.Value
            });
        }

        // امبليمنتاشن GetOrderSelectListAsync
        public async Task<IEnumerable<SelectListItem>> GetOrderSelectListAsync(Guid? selectedId = null)
        {
            var resp = await _orderService.GetByIdsAsync(Enumerable.Empty<Guid>());
            if (resp?.IsSuccess != true || resp.Result == null)
                return Enumerable.Empty<SelectListItem>();

            var list = JsonConvert
                .DeserializeObject<List<OrderDto>>(resp.Result.ToString())
                ?? new List<OrderDto>();

            return list.Select(o => new SelectListItem
            {
                Text = $"{o.OrderNumber} – {o.Party.Name}",
                Value = o.ID.ToString(),
                Selected = selectedId.HasValue && o.ID == selectedId.Value
            });
        }
    }
} 