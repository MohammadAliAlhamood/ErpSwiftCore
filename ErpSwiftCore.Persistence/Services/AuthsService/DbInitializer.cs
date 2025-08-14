using ErpSwiftCore.Domain.Entities.EntityAuth;
using ErpSwiftCore.Domain.IServices.IAuthsService;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.SharedKernel.ValueObjects;
using ErpSwiftCore.SharedKernel.Enums;
using ErpSwiftCore.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
namespace ErpSwiftCore.Persistence.Services.AuthsService
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        public DbInitializer(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext db)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void Initialize()
        {
            // 1) تطبيق المهاجرات إن وجدت
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                    _db.Database.Migrate();
            }
            catch
            {
                // لا نوقف التطبيق إذا فشلت الترحيلات هنا
            }

            // 2) إنشاء الأدوار الأساسية
            var roles = new[]
            {
                SD.Role_Admin,
                SD.Role_Company,
                SD.Role_AccountingEmployee,
                SD.Role_ManagementEmployee,
                SD.Role_RegularEmployee,
                SD.Role_WorkshopManager,
                SD.Role_SeniorManager,
                SD.Role_HRManager
            };
            foreach (var roleName in roles)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }

            // 3) إنشاء المستخدمين العامين (Admin, Accounting, Regular, إلخ)
            SeedDefaultUsers();

            // 4) إنشاء الشركات (Tenants) مع Contact و Address
            var seedCompanies = new[]
            {
                new Company
                {
                    CompanyName   = "Contoso Ltd",
                    Address       = new Address("1 Infinite Loop", "Cupertino", "CA", "95014", "USA"),
                    ContactInfo   = new Contact
                    {
                        Email   = "info@contoso.com",
                        Phone   = "408-555-0100",
                        Mobile  = "408-555-0101",
                        Fax     = "408-555-0102",
                        Website = "https://www.contoso.com"
                    },
                    IndustryType  = IndustryType.Technology,
                    WebsiteURL    = "https://www.contoso.com",
                    TaxID         = "TAX-123456",
                    CreatedAt     = DateTime.UtcNow,
                    CreatedBy     = Guid.Empty
                },
                new Company
                {
                    CompanyName   = "Fabrikam Inc",
                    Address       = new Address("100 Main Street", "Springfield", "IL", "62701", "USA"),
                    ContactInfo   = new Contact
                    {
                        Email   = "contact@fabrikam.com",
                        Phone   = "217-555-0200",
                        Mobile  = "217-555-0201",
                        Fax     = "217-555-0202",
                        Website = "https://www.fabrikam.com"
                    },
                    IndustryType  = IndustryType.Manufacturing,
                    WebsiteURL    = "https://www.fabrikam.com",
                    TaxID         = "TAX-654321",
                    CreatedAt     = DateTime.UtcNow,
                    CreatedBy     = Guid.Empty
                }
            };
            foreach (var comp in seedCompanies)
            {
                if (!_db.Companies.Any(c => c.CompanyName == comp.CompanyName))
                {
                    comp.ID = Guid.NewGuid();
                    _db.Companies.Add(comp);
                    _db.SaveChanges();
                }
            }

            // 5) إنشاء مستخدمين تابعين لكل شركة يدوياً
            // --- Contoso Ltd User ---
            var contoso = _db.Companies.AsNoTracking()
                              .FirstOrDefault(c => c.CompanyName == "Contoso Ltd");
            if (contoso != null && !_db.ApplicationUsers.Any(u => u.Email == "contoso@example.com"))
            {
                var contosoUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "contoso@example.com",
                    Email = "contoso@example.com",
                    NormalizedEmail = "CONTOSO@EXAMPLE.COM",
                    Name = "Contoso Ltd User",
                    TenantId = contoso.ID,
                    ProfilePictureUrl = null,
                    Address = contoso.Address?.ToString(),
                    PhoneNumber = contoso.ContactInfo?.Phone ?? "000-000-0000"
                };
                _userManager.CreateAsync(contosoUser, "Contoso@123").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(contosoUser, SD.Role_Company).GetAwaiter().GetResult();
            }

            // --- Fabrikam Inc User ---
            var fabrikam = _db.Companies.AsNoTracking()
                               .FirstOrDefault(c => c.CompanyName == "Fabrikam Inc");
            if (fabrikam != null && !_db.ApplicationUsers.Any(u => u.Email == "fabrikam@example.com"))
            {
                var fabrikamUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "fabrikam@example.com",
                    Email = "fabrikam@example.com",
                    NormalizedEmail = "FABRIKAM@EXAMPLE.COM",
                    Name = "Fabrikam Inc User",
                    TenantId = fabrikam.ID,
                    ProfilePictureUrl = null,
                    Address = fabrikam.Address?.ToString(),
                    PhoneNumber = fabrikam.ContactInfo?.Phone ?? "000-000-0000"
                };
                _userManager.CreateAsync(fabrikamUser, "Fabrikam@123").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(fabrikamUser, SD.Role_Company).GetAwaiter().GetResult();
            }
        }
        private void SeedDefaultUsers()
        {
            // 1. Admin
            if (!_db.ApplicationUsers.Any(u => u.Email == "admin@example.com"))
            {
                var admin = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    Name = "Admin User",
                    PhoneNumber = "1234567890",
                    Address = "123 Admin St"
                };
                _userManager.CreateAsync(admin, "Admin@123").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(admin, SD.Role_Admin).GetAwaiter().GetResult();
            }

            // 2. Accounting Employee
            if (!_db.ApplicationUsers.Any(u => u.Email == "accounting@example.com"))
            {
                var acc = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "accounting@example.com",
                    Email = "accounting@example.com",
                    NormalizedEmail = "ACCOUNTING@EXAMPLE.COM",
                    Name = "Accounting Employee",
                    PhoneNumber = "3456789012",
                    Address = "789 Accounting Rd"
                };
                _userManager.CreateAsync(acc, "Account@123").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(acc, SD.Role_AccountingEmployee).GetAwaiter().GetResult();
            }

            // 3. Regular Employee
            if (!_db.ApplicationUsers.Any(u => u.Email == "employee@example.com"))
            {
                var emp = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "employee@example.com",
                    Email = "employee@example.com",
                    NormalizedEmail = "EMPLOYEE@EXAMPLE.COM",
                    Name = "Regular Employee",
                    PhoneNumber = "4567890123",
                    Address = "101 Employee Blvd"
                };
                _userManager.CreateAsync(emp, "Employee@123").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(emp, SD.Role_RegularEmployee).GetAwaiter().GetResult();
            }

            // (يمكن إضافة المزيد هنا للأدوار الأخرى مثل WorkshopManager, SeniorManager, HRManager…)
        }
    }
}
