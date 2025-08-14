using ErpSwiftCore.Domain.AggregateRoots.AggregateInventoryRoots;
using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.Entities.EntityFinancial; 
using ErpSwiftCore.Domain.Entities.EntityInventory;
using ErpSwiftCore.Domain.Entities.EntityNotification;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.EntityCompany; 
using ErpSwiftCore.SharedKernel.Entities; 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; 
namespace ErpSwiftCore.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
        { 
        } 
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        // -------------------- Auth --------------------
        // DbSet للكلاسات الجديدة
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SecurityAlert> SecurityAlerts { get; set; }
        public DbSet<TrustedDevice> TrustedDevices { get; set; }
        public DbSet<SocialAccount> SocialAccounts { get; set; }


        // -------------------- Billing --------------------
        public DbSet<Invoice> Invoices { get; set; } 
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<InvoiceTax> InvoiceTaxes { get; set; }
        public DbSet<InvoiceDiscount> InvoiceDiscounts { get; set; }
        public DbSet<InvoiceApproval> InvoiceApprovals { get; set; }
        public DbSet<Party> Parties { get; set; }


        // -------------------- Finance --------------------

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalEntryLine> JournalEntryLines { get; set; } 
        public DbSet<CustomFinancialReportResult> CustomFinancialReportResults { get; set; } 
       


        // -------------------- Inventory --------------------
        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<InventoryAdjustment> InventoryAdjustments { get; set; }
        public DbSet<StockTransfer> StockTransfers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        // -------------------- Product --------------------
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBundle> ProductBundles { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductTax> ProductTaxes { get; set; }
        public DbSet<ProductUnitConversion> ProductUnitConversions { get; set; }

        // -------------------- Company (SharedKernel) --------------------
        public DbSet<Company> Companies { get; set; }

        public DbSet<CompanyBranch> Branches { get; set; }
        public DbSet<CompanySettings> CompanySettingses { get; set; }
        public DbSet<UnitOfMeasurement> UnitsOfMeasurement { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        // -------------------- Inventory --------------------
        public DbSet<InventoryPolicy> InventoryPolicies { get; set; }
         





        /// <summary>
        /// /////
        /// </summary>
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // -------------------- Apply Configurations Automatically --------------------
            //  modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // يطبّق كل Configurations (Entities + Seeds) بمرّة واحدة
            modelBuilder.ApplyAllConfigurations();

            // -------------------- Disable Cascade Delete Globally --------------------
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    if (!foreignKey.IsOwnership && foreignKey.DeleteBehavior == DeleteBehavior.Cascade)
                    {
                        foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                    }
                }
            }
             

            base.OnModelCreating(modelBuilder);
        }
    }
}