using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.IService.ICRMsService;
using ErpSwiftCore.Web.IService.IFinancialsService;
using ErpSwiftCore.Web.IService.IInventoriesService;
using ErpSwiftCore.Web.IService.IProductsService;
using ErpSwiftCore.Web.Mappings;
using ErpSwiftCore.Web.Service;
using ErpSwiftCore.Web.Service.AuthsService;
using ErpSwiftCore.Web.Service.BillingsService;
using ErpSwiftCore.Web.Service.CompaniesService;
using ErpSwiftCore.Web.Service.CRMsService;
using ErpSwiftCore.Web.Service.FinancialsService;
using ErpSwiftCore.Web.Service.InventoriesService;
using ErpSwiftCore.Web.Service.ProductsService;
using ErpSwiftCore.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

// AutoMapper registration (scans the executing assembly for profiles)
builder.Services.AddAutoMapperServices();

// Configure HTTP clients for remote API calls
builder.Services.AddHttpClient<IAuthenticationLocService, AuthenticationLocService>();
builder.Services.AddHttpClient<IPasswordSecurityService, PasswordSecurityService>();
builder.Services.AddHttpClient<IRoleService, RoleService>(); 
builder.Services.AddHttpClient<ISessionService, SessionService>(); 
builder.Services.AddHttpClient<ITokenProvider, TokenProvider>(); 
builder.Services.AddHttpClient<IUserProfileService, UserProfileService>();


builder.Services.AddHttpClient<ICompanyService, CompanyService>();
builder.Services.AddHttpClient<ICompanyBranchService, CompanyBranchService>();

builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();
builder.Services.AddHttpClient<IUnitOfMeasurementService, UnitOfMeasurementService>();


builder.Services.AddHttpClient<IProductBundleService, ProductBundleService>();
builder.Services.AddHttpClient<IProductCategoryService, ProductCategoryService>();
builder.Services.AddHttpClient<IProductPriceService, ProductPriceService>();
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<IProductTaxService, ProductTaxService>();
builder.Services.AddHttpClient<IProductUnitConversionService, ProductUnitConversionService>();



// Inventories HTTP clients
builder.Services.AddHttpClient<IInventoryService, InventoryService>();
builder.Services.AddHttpClient<IAdjustmentService, AdjustmentService>();
builder.Services.AddHttpClient<IInventoryPolicyService, InventoryPolicyService>();
builder.Services.AddHttpClient<IInventoryTransactionService, InventoryTransactionService>();
builder.Services.AddHttpClient<IStocksTransferService, StocksTransferService>();
builder.Services.AddHttpClient<IWarehouseService, WarehouseService>();



// Billing HTTP clients
builder.Services.AddHttpClient<IInvoiceService, InvoiceService>();
builder.Services.AddHttpClient<IInvoiceTaxDiscountService, InvoiceTaxDiscountService>();
builder.Services.AddHttpClient<IOrderService, OrderService>();
builder.Services.AddHttpClient<IPaymentService, PaymentService>();


// Financial  HTTP clients

builder.Services.AddHttpClient<IAccountService , AccountService>();
builder.Services.AddHttpClient<ICostCenterService, CostCenterService>();
builder.Services.AddHttpClient<ICustomFinancialReportResultService, CustomFinancialReportResultService>();
builder.Services.AddHttpClient<IJournalEntryService, JournalEntryService>();



builder.Services.AddHttpClient<ICustomerService, CustomerService>();
builder.Services.AddHttpClient<ISupplierService, SupplierService>();






// Base URL for ERP API calls, read from configuration
SD.ErpAPIBase = builder.Configuration["ServiceUrls:ErpAPI"];

// Scoped service registrations
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IBaseService, BaseService>();


// Configure HTTP clients for remote API calls
builder.Services.AddScoped<IAuthenticationLocService, AuthenticationLocService>();
builder.Services.AddScoped<IPasswordSecurityService, PasswordSecurityService>();
builder.Services.AddScoped<IRoleService, RoleService>(); 
builder.Services.AddScoped<ISessionService, SessionService>(); 
builder.Services.AddScoped<ITokenProvider, TokenProvider>(); 
builder.Services.AddScoped<IUserProfileService, UserProfileService>();



builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyBranchService, CompanyBranchService>();


builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IUnitOfMeasurementService, UnitOfMeasurementService>();


builder.Services.AddScoped<IProductBundleService, ProductBundleService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductPriceService, ProductPriceService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductTaxService, ProductTaxService>();
builder.Services.AddScoped<IProductUnitConversionService, ProductUnitConversionService>();




// Inventories scoped services
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IAdjustmentService, AdjustmentService>();
builder.Services.AddScoped<IInventoryPolicyService, InventoryPolicyService>();
builder.Services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();
builder.Services.AddScoped<IStocksTransferService, StocksTransferService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();





// Billing scoped services
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceTaxDiscountService, InvoiceTaxDiscountService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();


// Financial  scoped services

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICostCenterService, CostCenterService>();
builder.Services.AddScoped<ICustomFinancialReportResultService, CustomFinancialReportResultService>();
builder.Services.AddScoped<IJournalEntryService, JournalEntryService>();



builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();



builder.Services.AddScoped<ISelectListService, SelectListService>();




builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Authentication/Login";
        options.AccessDeniedPath = "/Authentication/AccessDenied";
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
