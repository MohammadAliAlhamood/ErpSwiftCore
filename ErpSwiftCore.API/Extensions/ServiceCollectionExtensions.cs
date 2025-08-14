using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceService;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceTaxDiscountService;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using ErpSwiftCore.Domain.IServices.IBillingService.IPaymentService;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyService;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService;
using ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using ErpSwiftCore.Domain.IServices.IFinancialService.IAccountService;
using ErpSwiftCore.Domain.IServices.IFinancialService.ICostCenterService;
using ErpSwiftCore.Domain.IServices.IFinancialService.IFinancialReportService;
using ErpSwiftCore.Domain.IServices.IFinancialService.IJournalEntryService;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryPolicyService;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryService;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryTransactionService;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IWarehouseService;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductBundleService;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductPriceService;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductService;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
using ErpSwiftCore.Notifications;
using ErpSwiftCore.Notifications.Events;
using ErpSwiftCore.Notifications.Interface;
using ErpSwiftCore.Persistence.Services.AuthsService;
using ErpSwiftCore.Persistence.Services.BillingService.InvoiceReportingService;
using ErpSwiftCore.Persistence.Services.BillingService.InvoiceService;
using ErpSwiftCore.Persistence.Services.BillingService.InvoiceTaxDiscountService;
using ErpSwiftCore.Persistence.Services.BillingService.OrderService;
using ErpSwiftCore.Persistence.Services.BillingService.PaymentService;
using ErpSwiftCore.Persistence.Services.CompanyService.CompanyBranchService;
using ErpSwiftCore.Persistence.Services.CompanyService.CompanyService;
using ErpSwiftCore.Persistence.Services.CompanyService.CompanySettingsService;
using ErpSwiftCore.Persistence.Services.CompanyService.CurrencyService;
using ErpSwiftCore.Persistence.Services.CompanyService.UnitOfMeasurementService;
using ErpSwiftCore.Persistence.Services.CRMService.CustomerService;
using ErpSwiftCore.Persistence.Services.CRMService.SupplierService;
using ErpSwiftCore.Persistence.Services.FinancialService.AccountService;
using ErpSwiftCore.Persistence.Services.FinancialService.CostCenterService;
using ErpSwiftCore.Persistence.Services.FinancialService.FinancialReportService;
using ErpSwiftCore.Persistence.Services.FinancialService.JournalEntryService;
using ErpSwiftCore.Persistence.Services.InventoryService.InventoryAdjustmentService;
using ErpSwiftCore.Persistence.Services.InventoryService.InventoryPolicyService;
using ErpSwiftCore.Persistence.Services.InventoryService.InventoryService;
using ErpSwiftCore.Persistence.Services.InventoryService.InventoryTransactionService;
using ErpSwiftCore.Persistence.Services.InventoryService.StockTransferService;
using ErpSwiftCore.Persistence.Services.InventoryService.WarehouseService;
using ErpSwiftCore.Persistence.Services.ProductsService.ProductBundleService;
using ErpSwiftCore.Persistence.Services.ProductsService.ProductCategoryService;
using ErpSwiftCore.Persistence.Services.ProductsService.ProductPriceService;
using ErpSwiftCore.Persistence.Services.ProductsService.ProductService;
using ErpSwiftCore.Persistence.Services.ProductsService.ProductTaxService;
using ErpSwiftCore.Persistence.Services.ProductsService.ProductUnitConversionService;
namespace ErpSwiftCore.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection services)
        {
            services.AddScoped<IActivityLogsService, ActivityLogsService>();
            services.AddScoped<IAuthenticationLocService, AuthenticationLocService>();
            services.AddScoped<IPasswordSecurityService, PasswordSecurityService>();
            services.AddScoped<IRefreshConfirmTokenService, RefreshConfirmTokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISocialSSOService, SocialSSOService>();
            services.AddScoped<ITwoFactorService, TwoFactorService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            return services;
        }
        public static IServiceCollection AddNotificationInfrastructure(this IServiceCollection services)
        {
            // MediatR dispatcher
            services.AddScoped<IEventDispatcher, MediatRDispatcher>();

            // Transport delegate
            services.AddScoped<NotificationTransportDelegate>(sp => async notification =>
            {
                var logger = sp.GetRequiredService<ILogger<Program>>();
                logger.LogInformation($"[Transport] Sending notification to {notification.RecipientId}: {notification.Title}");
                return true;
            });

            // Inventory notifications
            services.AddScoped<IInventoryNotificationRecipientService, InventoryNotificationRecipientService>();

            // General notification service
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
        public static IServiceCollection AddInvoiceServices(this IServiceCollection services)
        {
            services.AddScoped<IInvoiceCommandService, InvoiceCommandService>();
            services.AddScoped<IInvoiceQueryService, InvoiceQueryService>();
            services.AddScoped<IInvoiceValidationService, InvoiceValidationService>();

            services.AddScoped<IInvoiceTaxDiscountCommandService, InvoiceTaxDiscountCommandService>();
            services.AddScoped<IInvoiceTaxDiscountQueryService, InvoiceTaxDiscountQueryService>();
            services.AddScoped<IInvoiceTaxDiscountValidationService, InvoiceTaxDiscountValidationService>();

            services.AddScoped<IInvoiceReportingQueryService, InvoiceReportingQueryService>();
            services.AddScoped<IInvoiceImportExportCommandService, InvoiceImportExportCommandService>();


            services.AddScoped<IOrderCommandService, OrderCommandService>();
            services.AddScoped<IOrderQueryService, OrderQueryService>();
            services.AddScoped<IOrderValidationService, OrderValidationService>();



            services.AddScoped<IPaymentCommandService, PaymentCommandService>();
            services.AddScoped<IPaymentQueryService, PaymentQueryService>();
            services.AddScoped<IPaymentValidationService, PaymentValidationService>();

            return services;
        }
        public static IServiceCollection AddCompanyServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyCommandService, CurrencyCommandService>();
            services.AddScoped<ICurrencyQueryService, CurrencyQueryService>();
            services.AddScoped<ICurrencyValidationService, CurrencyValidationService>();

            services.AddScoped<IUnitOfMeasurementCommandService, UnitOfMeasurementCommandService>();
            services.AddScoped<IUnitOfMeasurementQueryService, UnitOfMeasurementQueryService>();

            services.AddScoped<ICompanyCommandService, CompanyCommandService>();
            services.AddScoped<ICompanyQueryService, CompanyQueryService>();
            services.AddScoped<ICompanyValidationService, CompanyValidationService>();

            services.AddScoped<ICompanyBranchCommandService, CompanyBranchCommandService>();
            services.AddScoped<ICompanyBranchQueryService, CompanyBranchQueryService>();
            services.AddScoped<ICompanyBranchValidationService, CompanyBranchValidationService>();

            services.AddScoped<ICompanySettingsCommandService, CompanySettingsCommandService>();
            services.AddScoped<ICompanySettingsQueryService, CompanySettingsQueryService>();
            services.AddScoped<ICompanySettingsValidationService, CompanySettingsValidationService>();

            return services;
        }
        public static IServiceCollection AddCrmServices(this IServiceCollection services)
        { 

            services.AddScoped<ICustomerCommandService, CustomerCommandService>();
            services.AddScoped<ICustomerQueryService, CustomerQueryService>();
            services.AddScoped<ICustomerValidationService, CustomerValidationService>();
             

            services.AddScoped<ISupplierCommandService, SupplierCommandService>();
            services.AddScoped<ISupplierQueryService, SupplierQueryService>();
            services.AddScoped<ISupplierValidationService, SupplierValidationService>();

            return services;
        }
        public static IServiceCollection AddFinancialServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountCommandService, AccountCommandService>();
            services.AddScoped<IAccountQueryService, AccountQueryService>();
            services.AddScoped<IAccountValidationService, AccountValidationService>();
             

            services.AddScoped<ICostCenterCommandService, CostCenterCommandService>();
            services.AddScoped<ICostCenterQueryService, CostCenterQueryService>();
            services.AddScoped<ICostCenterValidationService, CostCenterValidationService>();
             

            services.AddScoped<IFinancialReportCommandService, FinancialReportCommandService>();
            services.AddScoped<IFinancialReportQueryService, FinancialReportQueryService>();
            services.AddScoped<IFinancialReportValidationService, FinancialReportValidationService>();
             
            services.AddScoped<IJournalEntryCommandService, JournalEntryCommandService>();
            services.AddScoped<IJournalEntryQueryService, JournalEntryQueryService>();
            services.AddScoped<IJournalEntryValidationService, JournalEntryValidationService>();
             

            return services;
        }
        public static IServiceCollection AddInventoryServices(this IServiceCollection services)
        {
            services.AddScoped<IInventoryAdjustmentCommandService, InventoryAdjustmentCommandService>();
            services.AddScoped<IInventoryAdjustmentQueryService, InventoryAdjustmentQueryService>();
            services.AddScoped<IInventoryAdjustmentValidationService, InventoryAdjustmentValidationService>();

            services.AddScoped<IInventoryPolicyCommandService, InventoryPolicyCommandService>();
            services.AddScoped<IInventoryPolicyQueryService, InventoryPolicyQueryService>(); 

            services.AddScoped<IInventoryQueryService, InventoryQueryService>();
            services.AddScoped<IInventoryTransactionQueryService, InventoryTransactionQueryService>();

            services.AddScoped<IWarehouseCommandService, WarehouseCommandService>();
            services.AddScoped<IWarehouseQueryService, WarehouseQueryService>();
            services.AddScoped<IWarehouseValidationService, WarehouseValidationService>();

            services.AddScoped<IStockTransferCommandService, StockTransferCommandService>();
            services.AddScoped<IStockTransferQueryService, StockTransferQueryService>();
            services.AddScoped<IStockTransferValidationService, StockTransferValidationService>();

            return services;
        }
        public static IServiceCollection AddProductServices(this IServiceCollection services)
        {
            services.AddScoped<IProductBundleCommandService, ProductBundleCommandService>();
            services.AddScoped<IProductBundleQueryService, ProductBundleQueryService>();
            services.AddScoped<IProductBundleValidationService, ProductBundleValidationService>();

            services.AddScoped<IProductCategoryCommandService, ProductCategoryCommandService>();
            services.AddScoped<IProductCategoryQueryService, ProductCategoryQueryService>();
            services.AddScoped<IProductCategoryValidationService, ProductCategoryValidationService>();

            services.AddScoped<IProductPriceCommandService, ProductPriceCommandService>();
            services.AddScoped<IProductPriceQueryService, ProductPriceQueryService>();
            services.AddScoped<IProductPriceValidationService, ProductPriceValidationService>();

            services.AddScoped<IProductCommandService, ProductCommandService>();
            services.AddScoped<IProductQueryService, ProductQueryService>();
            services.AddScoped<IProductValidationService, ProductValidationService>();

            services.AddScoped<IProductTaxCommandService, ProductTaxCommandService>();
            services.AddScoped<IProductTaxQueryService, ProductTaxQueryService>();
            services.AddScoped<IProductTaxValidationService, ProductTaxValidationService>();

            services.AddScoped<IProductUnitConversionCommandService, ProductUnitConversionCommandService>();
            services.AddScoped<IProductUnitConversionQueryService, ProductUnitConversionQueryService>();
            services.AddScoped<IProductUnitConversionValidationService, ProductUnitConversionValidationService>();

            return services;
        }
        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            return services;
        }
    }
}