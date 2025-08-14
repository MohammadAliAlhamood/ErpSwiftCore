using ErpSwiftCore.Domain.IRepositories.IBillingRepositories;
using ErpSwiftCore.Domain.IRepositories.ICRMRepositories;
using ErpSwiftCore.Domain.IRepositories.IFinancialRepositories; 
using ErpSwiftCore.Domain.IRepositories.IInventoryRepositories;
using ErpSwiftCore.Domain.IRepositories.ILoggingRepositories;
using ErpSwiftCore.Domain.IRepositories.INotificationRepositories;
using ErpSwiftCore.Domain.IRepositories.IProductRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ErpSwiftCore.Domain.IRepositories
{
    public interface IMultiTenantUnitOfWork : IDisposable
    {
        ILogEntryRepository LogEntry { get; }
        INotificationRepository Notification { get; }
        IOrderRepository Order { get; }
        IOrderLineRepository OrderLine { get; }
        IAccountRepository Account { get; } 
        ICostCenterRepository CostCenter { get; } 
        IJournalEntryRepository JournalEntry { get; }
        IJournalEntryLineRepository JournalEntryLine { get; }
        IProductUnitConversionRepository ProductUnitConversion { get; } 
        ICustomFinancialReportResultRepository CustomFinancialReportResult { get; }

      

        // Sales & Purchases
        IInvoiceRepository Invoice { get; }

        IInvoiceLineRepository InvoiceLine { get; }
        IInvoiceTaxRepository InvoiceTax { get; }
        IInvoiceDiscountRepository InvoiceDiscount { get; }
        IInvoiceApprovalRepository InvoiceApproval { get; } 

        IPaymentRepository Payment { get; }
        IPartyRepository Party { get; }


        // Inventory
        IInventoryRepository Inventory { get; }

        IInventoryTransactionRepository InventoryTransaction { get; }
        IInventoryAdjustmentRepository InventoryAdjustment { get; }
        IStockTransferRepository StockTransfer { get; }
        IWarehouseRepository Warehouse { get; }
        IProductRepository Product { get; }
        IProductBundleRepository ProductBundle { get; }
        IProductCategoryRepository ProductCategory { get; }
        IProductPriceRepository ProductPrice { get; }
        IProductTaxRepository ProductTax { get; }
        IInventoryPolicyRepository InventoryPolicy { get; }


        // CRM
        ICustomerRepository Customer { get; }

        ISupplierRepository Supplier { get; } 

        Task SaveAsync();

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}