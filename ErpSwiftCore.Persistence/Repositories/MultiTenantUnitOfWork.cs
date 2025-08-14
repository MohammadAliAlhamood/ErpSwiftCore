using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.Entities.EntityFinancial; 
using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.Entities.EntityNotification;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IRepositories.IBillingRepositories;
using ErpSwiftCore.Domain.IRepositories.ICRMRepositories;
using ErpSwiftCore.Domain.IRepositories.IFinancialRepositories; 
using ErpSwiftCore.Domain.IRepositories.IInventoryRepositories;
using ErpSwiftCore.Domain.IRepositories.ILoggingRepositories;
using ErpSwiftCore.Domain.IRepositories.INotificationRepositories;
using ErpSwiftCore.Domain.IRepositories.IProductRepositories; 
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Repositories.BillingRepositories;
using ErpSwiftCore.Persistence.Repositories.CRMRepositories;
using ErpSwiftCore.Persistence.Repositories.FinancialRepositories; 
using ErpSwiftCore.Persistence.Repositories.InventoryRepositories;
using ErpSwiftCore.Persistence.Repositories.NotificationRepositories;
using ErpSwiftCore.Persistence.Repositories.ProductRepositories;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using ErpSwiftCore.Persistence.Repositories.LoggingRepositories;
using ErpSwiftCore.Domain.Entities.EntityLogging;
namespace ErpSwiftCore.Persistence.Repositories
{
    public class MultiTenantUnitOfWork : IMultiTenantUnitOfWork
    {
        private readonly AppDbContext _db;
        private readonly ITenantProvider _tenantProvider;
        private readonly IUserProvider _userProvider;

        public INotificationRepository Notification { get; }
        // Financial


        public ILogEntryRepository LogEntry { get; }
        public IAccountRepository Account { get; }
        public ICostCenterRepository CostCenter { get; }
        public IJournalEntryRepository JournalEntry { get; }
        public IJournalEntryLineRepository JournalEntryLine { get; }
        public IProductUnitConversionRepository ProductUnitConversion { get; }
        public ICustomFinancialReportResultRepository CustomFinancialReportResult { get; }

      

        // Sales & Purchases
        public IInvoiceRepository Invoice { get; }
        public IInvoiceLineRepository InvoiceLine { get; }
        public IInvoiceTaxRepository InvoiceTax { get; }
        public IInvoiceDiscountRepository InvoiceDiscount { get; }
        public IInvoiceApprovalRepository InvoiceApproval { get; }

        public IPaymentRepository Payment { get; }

        // Inventory
        public IInventoryRepository Inventory { get; }
        public IInventoryTransactionRepository InventoryTransaction { get; }
        public IInventoryAdjustmentRepository InventoryAdjustment { get; }
        public IStockTransferRepository StockTransfer { get; }
        public IWarehouseRepository Warehouse { get; }
        public IProductRepository Product { get; }
        public IProductBundleRepository ProductBundle { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IProductPriceRepository ProductPrice { get; }
        public IProductTaxRepository ProductTax { get; }
        public IInventoryPolicyRepository InventoryPolicy { get; }

        // CRM
        public ICustomerRepository Customer { get; }
        public ISupplierRepository Supplier { get; } 

        public IOrderRepository Order { get; }
        public IOrderLineRepository OrderLine { get; }

        public IPartyRepository Party { get; }

        public MultiTenantUnitOfWork(
            AppDbContext db,
            ITenantProvider tenantProvider,
            IUserProvider userProvider,

            // Financial Include Validators
            IIncludeValidator<Account> accountIncludeValidator,
            IIncludeValidator<CostCenter> costCenterIncludeValidator,
            IIncludeValidator<JournalEntry> journalEntryIncludeValidator,
            IIncludeValidator<JournalEntryLine> journalEntryLineIncludeValidator,
            IIncludeValidator<ProductUnitConversion> productUnitConversionIncludeValidator,
            IIncludeValidator<CustomFinancialReportResult> customFinancialReportResultIncludeValidator,
             

            // Billing Include Validators
            IIncludeValidator<Invoice> invoiceIncludeValidator,
            IIncludeValidator<InvoiceLine> invoiceLineIncludeValidator,
            IIncludeValidator<InvoiceTax> invoiceTaxIncludeValidator,
            IIncludeValidator<InvoiceDiscount> invoiceDiscountIncludeValidator,
            IIncludeValidator<InvoiceApproval> invoiceApprovalIncludeValidator, 
            IIncludeValidator<Payment> paymentIncludeValidator,
            IIncludeValidator<Party> partyIncludeValidator,

            // Inventory Include Validators
            IIncludeValidator<Inventory> inventoryIncludeValidator,
            IIncludeValidator<InventoryTransaction> inventoryTransactionIncludeValidator,
            IIncludeValidator<InventoryAdjustment> inventoryAdjustmentIncludeValidator,
            IIncludeValidator<StockTransfer> stockTransferIncludeValidator,
            IIncludeValidator<Warehouse> warehouseIncludeValidator,
            IIncludeValidator<Product> productIncludeValidator,
            IIncludeValidator<ProductBundle> productBundleIncludeValidator,
            IIncludeValidator<ProductCategory> productCategoryIncludeValidator,
            IIncludeValidator<ProductPrice> productPriceIncludeValidator,
            IIncludeValidator<ProductTax> productTaxIncludeValidator,
            IIncludeValidator<InventoryPolicy> inventoryPolicyIncludeValidator,

            // CRM Include Validators
            IIncludeValidator<Customer> customerIncludeValidator,
            IIncludeValidator<Supplier> supplierIncludeValidator, 

            // Misc Include Validators
            IIncludeValidator<Order> orderIncludeValidator,
            IIncludeValidator<OrderLine> orderLineIncludeValidator,
            IIncludeValidator<Notification> notificationIncludeValidator,
            IIncludeValidator<LogEntry> logentryIncludeValidator


            )
        {
            _db = db;
            _tenantProvider = tenantProvider;
            _userProvider = userProvider;

            LogEntry =new LogEntryRepository(_db, _tenantProvider, _userProvider, logentryIncludeValidator);
            // Financial
            Account = new AccountRepository(_db, _tenantProvider, _userProvider, accountIncludeValidator);
            CostCenter = new CostCenterRepository(_db, _tenantProvider, _userProvider, costCenterIncludeValidator);
            JournalEntry = new JournalEntryRepository(_db, _tenantProvider, _userProvider, journalEntryIncludeValidator);
            JournalEntryLine = new JournalEntryLineRepository(_db, _tenantProvider, _userProvider, journalEntryLineIncludeValidator);
            ProductUnitConversion = new ProductUnitConversionRepository(_db, _tenantProvider, _userProvider, productUnitConversionIncludeValidator);
            CustomFinancialReportResult = new CustomFinancialReportResultRepository(_db, _tenantProvider, _userProvider, customFinancialReportResultIncludeValidator);


            
            // Sales & Purchases
            Invoice = new InvoiceRepository(_db, _tenantProvider, _userProvider, invoiceIncludeValidator);
            InvoiceLine = new InvoiceLineRepository(_db, _tenantProvider, _userProvider, invoiceLineIncludeValidator);
            InvoiceTax = new InvoiceTaxRepository(_db, _tenantProvider, _userProvider, invoiceTaxIncludeValidator);
            InvoiceDiscount = new InvoiceDiscountRepository(_db, _tenantProvider, _userProvider, invoiceDiscountIncludeValidator);
            InvoiceApproval = new InvoiceApprovalRepository(_db, _tenantProvider, _userProvider, invoiceApprovalIncludeValidator);
            Payment = new PaymentRepository(_db, _tenantProvider, _userProvider, paymentIncludeValidator);
            Party = new PartyRepository(_db, _tenantProvider, _userProvider, partyIncludeValidator);

            // Inventory
            Inventory = new InventoryRepository(_db, _tenantProvider, _userProvider, inventoryIncludeValidator);
            InventoryTransaction = new InventoryTransactionRepository(_db, _tenantProvider, _userProvider, inventoryTransactionIncludeValidator);
            InventoryAdjustment = new InventoryAdjustmentRepository(_db, _tenantProvider, _userProvider, inventoryAdjustmentIncludeValidator);
            StockTransfer = new StockTransferRepository(_db, _tenantProvider, _userProvider, stockTransferIncludeValidator);
            Warehouse = new WarehouseRepository(_db, _tenantProvider, _userProvider, warehouseIncludeValidator);
            Product = new ProductRepository(_db, _tenantProvider, _userProvider, productIncludeValidator);
            ProductBundle = new ProductBundleRepository(_db, _tenantProvider, _userProvider, productBundleIncludeValidator);
            ProductCategory = new ProductCategoryRepository(_db, _tenantProvider, _userProvider, productCategoryIncludeValidator);
            ProductPrice = new ProductPriceRepository(_db, _tenantProvider, _userProvider, productPriceIncludeValidator);
            ProductTax = new ProductTaxRepository(_db, _tenantProvider, _userProvider, productTaxIncludeValidator);
            InventoryPolicy = new InventoryPolicyRepository(_db, _tenantProvider, _userProvider, inventoryPolicyIncludeValidator);


            // CRM
            Customer = new CustomerRepository(_db, _tenantProvider, _userProvider, customerIncludeValidator);
            Supplier = new SupplierRepository(_db, _tenantProvider, _userProvider, supplierIncludeValidator);
                  Order = new OrderRepository(_db, _tenantProvider, _userProvider, orderIncludeValidator);
            OrderLine = new OrderLineRepository(_db, _tenantProvider, _userProvider, orderLineIncludeValidator);
            Notification = new NotificationRepository(_db, _tenantProvider, _userProvider, notificationIncludeValidator);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Database.BeginTransactionAsync(cancellationToken);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
