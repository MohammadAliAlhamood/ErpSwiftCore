using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ErpSwiftCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "logging");

            migrationBuilder.EnsureSchema(
                name: "notification");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndustryType = table.Column<int>(type: "int", nullable: false),
                    WebsiteURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TaxID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LogoURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitsOfMeasurement",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsOfMeasurement", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Currency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParentAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Account_ParentAccount",
                        column: x => x.ParentAccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsSubscribedToSecurityNotifications = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Companies_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Branches_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostCenters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CenterName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CostCenters_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CostCenters_CostCenters_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CostCenters",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Customers_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomFinancialReportResults",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFinancialReportResults", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomFinancialReportResults_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "JournalEntries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntryNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JournalEntries_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                schema: "logging",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OccurredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "UTC timestamp when the event occurred"),
                    Level = table.Column<int>(type: "int", nullable: false, comment: "Severity level as int"),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Category or area of the event, e.g. 'Inventory', 'Auth'"),
                    Operation = table.Column<int>(type: "int", nullable: false, comment: "Operation type as int"),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Name of the entity affected, e.g. 'Budget'"),
                    EntityKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Identifier of the affected entity as string"),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Short message describing the event"),
                    DetailsJson = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "JSON with additional details (old/new values, context)"),
                    CorrelationId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Correlation ID to trace related operations"),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "Source of the event, e.g. service or machine name"),
                    Tag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "Optional tag or small contextual code"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LogEntries_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                schema: "notification",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Channel = table.Column<int>(type: "int", nullable: false, comment: "Delivery channel as int"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "Notification type as int"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PayloadJson = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "JSON payload with additional data"),
                    Priority = table.Column<int>(type: "int", nullable: false, comment: "Priority as int"),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LastTriedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CorrelationId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Status as int"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notifications_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategories_ProductCategories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaxSupplyLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    NationalID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInfo_Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Suppliers_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderType = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    PartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartyType = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanySettingses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DefaultLanguage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DefaultARAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultRevenueAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultTaxPayableAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultDiscountsAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalaryExpenseAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrollPayableAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrollDeductionsAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultCashAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySettingses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Accounts_DefaultARAccountId",
                        column: x => x.DefaultARAccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Accounts_DefaultCashAccountId",
                        column: x => x.DefaultCashAccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Accounts_DefaultDiscountsAccountId",
                        column: x => x.DefaultDiscountsAccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Accounts_DefaultRevenueAccountId",
                        column: x => x.DefaultRevenueAccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Accounts_DefaultTaxPayableAccountId",
                        column: x => x.DefaultTaxPayableAccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Accounts_PayrollDeductionsAccountId",
                        column: x => x.PayrollDeductionsAccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Accounts_PayrollPayableAccountId",
                        column: x => x.PayrollPayableAccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Accounts_SalaryExpenseAccountId",
                        column: x => x.SalaryExpenseAccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanySettingses_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    DeviceInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityAlerts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AlertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityAlerts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SecurityAlerts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeviceInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sessions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialAccounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProviderUserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialAccounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SocialAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrustedDevices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrustedDevices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TrustedDevices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BranchID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsStorage = table.Column<bool>(type: "bit", nullable: false),
                    IsOperational = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Warehouses_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouses_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalEntryLines",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JournalEntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDebit = table.Column<bool>(type: "bit", nullable: false),
                    CostCenterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalEntryLines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_JournalEntryLines_JournalEntries_JournalEntryId",
                        column: x => x.JournalEntryId,
                        principalTable: "JournalEntries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_UnitsOfMeasurement_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalTable: "UnitsOfMeasurement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceStatus = table.Column<int>(type: "int", nullable: false),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceType = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Invoices_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityOnHand1 = table.Column<int>(type: "int", nullable: false),
                    QuantityReserved = table.Column<int>(type: "int", nullable: false),
                    QuantityOnHand = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inventories_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventories_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventories_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAdjustments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityChanged = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    AdjustmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAdjustments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustments_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustments_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryAdjustments_Warehouses_WarehouseID",
                        column: x => x.WarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderLines_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductBundles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfMeasurementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBundles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductBundles_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBundles_Products_ComponentProductId",
                        column: x => x.ComponentProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBundles_Products_ParentProductId",
                        column: x => x.ParentProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBundles_UnitsOfMeasurement_UnitOfMeasurementId",
                        column: x => x.UnitOfMeasurementId,
                        principalTable: "UnitsOfMeasurement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceType = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTaxes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTaxes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductTaxes_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTaxes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductUnitConversions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Factor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnitConversions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductUnitConversions_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductUnitConversions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductUnitConversions_UnitsOfMeasurement_FromUnitId",
                        column: x => x.FromUnitId,
                        principalTable: "UnitsOfMeasurement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductUnitConversions_UnitsOfMeasurement_ToUnitId",
                        column: x => x.ToUnitId,
                        principalTable: "UnitsOfMeasurement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockTransfers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToWarehouseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransfers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockTransfers_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransfers_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransfers_Warehouses_FromWarehouseID",
                        column: x => x.FromWarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransfers_Warehouses_ToWarehouseID",
                        column: x => x.ToWarehouseID,
                        principalTable: "Warehouses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceApprovals",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceApprovals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvoiceApprovals_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceApprovals_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDiscounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDiscounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvoiceDiscounts_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceDiscounts_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTaxes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTaxes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvoiceTaxes_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceTaxes_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsReconciled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payments_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryPolicies",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyType = table.Column<int>(type: "int", nullable: false),
                    ReorderLevel = table.Column<int>(type: "int", nullable: false),
                    MaxStockLevel = table.Column<int>(type: "int", nullable: false),
                    AutoReorderEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AutoReorderQuantity = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CurrentStock = table.Column<int>(type: "int", nullable: true),
                    IsAutoReorderEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryPolicies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryPolicies_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryPolicies_Inventories_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransactions",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RunningBalance = table.Column<int>(type: "int", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RelatedJournalEntryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_Inventories_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransactions_JournalEntries_RelatedJournalEntryID",
                        column: x => x.RelatedJournalEntryID,
                        principalTable: "JournalEntries",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "ID", "CreatedAt", "CreatedBy", "CurrencyCode", "CurrencyName", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "USD", "US Dollar", null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "EUR", "Euro", null, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "JPY", "Japanese Yen", null, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "GBP", "British Pound", null, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "AUD", "Australian Dollar", null, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "CAD", "Canadian Dollar", null, null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "CHF", "Swiss Franc", null, null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "CNY", "Chinese Yuan", null, null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "SEK", "Swedish Krona", null, null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "NZD", "New Zealand Dollar", null, null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "MXN", "Mexican Peso", null, null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "SGD", "Singapore Dollar", null, null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "HKD", "Hong Kong Dollar", null, null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "NOK", "Norwegian Krone", null, null },
                    { new Guid("f1f1f1f1-f1f1-f1f1-f1f1-f1f1f1f1f1f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "KRW", "South Korean Won", null, null },
                    { new Guid("f2f2f2f2-f2f2-f2f2-f2f2-f2f2f2f2f2f2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "TRY", "Turkish Lira", null, null },
                    { new Guid("f3f3f3f3-f3f3-f3f3-f3f3-f3f3f3f3f3f3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "RUB", "Russian Ruble", null, null },
                    { new Guid("f4f4f4f4-f4f4-f4f4-f4f4-f4f4f4f4f4f4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "INR", "Indian Rupee", null, null },
                    { new Guid("f5f5f5f5-f5f5-f5f5-f5f5-f5f5f5f5f5f5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "BRL", "Brazilian Real", null, null },
                    { new Guid("f6f6f6f6-f6f6-f6f6-f6f6-f6f6f6f6f6f6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "ZAR", "South African Rand", null, null },
                    { new Guid("f7f7f7f7-f7f7-f7f7-f7f7-f7f7f7f7f7f7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "DKK", "Danish Krone", null, null },
                    { new Guid("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "PLN", "Polish Zloty", null, null },
                    { new Guid("f9f9f9f9-f9f9-f9f9-f9f9-f9f9f9f9f9f9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "THB", "Thai Baht", null, null },
                    { new Guid("fafafafa-fafa-fafa-fafa-fafafafafafa"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "IDR", "Indonesian Rupiah", null, null },
                    { new Guid("fbfbfbfb-fbfb-fbfb-fbfb-fbfbfbfbfbfb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "MYR", "Malaysian Ringgit", null, null }
                });

            migrationBuilder.InsertData(
                table: "UnitsOfMeasurement",
                columns: new[] { "ID", "Abbreviation", "CreatedAt", "CreatedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "kg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Weight unit", "Kilogram", null, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "m", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Length unit", "Meter", null, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "g", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Weight unit", "Gram", null, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "mg", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Weight unit", "Milligram", null, null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "t", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Weight unit (1000 kg)", "Ton", null, null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "mm", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Length unit", "Millimeter", null, null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "cm", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Length unit", "Centimeter", null, null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "km", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Length unit", "Kilometer", null, null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "in", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Length unit", "Inch", null, null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "ft", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Length unit", "Foot", null, null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "yd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Length unit", "Yard", null, null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "mi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Length unit", "Mile", null, null },
                    { new Guid("f1f1f1f1-f1f1-f1f1-f1f1-f1f1f1f1f1f1"), "L", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Volume unit", "Liter", null, null },
                    { new Guid("f2f2f2f2-f2f2-f2f2-f2f2-f2f2f2f2f2f2"), "mL", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Volume unit", "Milliliter", null, null },
                    { new Guid("f3f3f3f3-f3f3-f3f3-f3f3-f3f3f3f3f3f3"), "m³", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Volume unit", "Cubic Meter", null, null },
                    { new Guid("f4f4f4f4-f4f4-f4f4-f4f4-f4f4f4f4f4f4"), "cm³", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Volume unit", "Cubic Centimeter", null, null },
                    { new Guid("f5f5f5f5-f5f5-f5f5-f5f5-f5f5f5f5f5f5"), "m²", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Area unit", "Square Meter", null, null },
                    { new Guid("f6f6f6f6-f6f6-f6f6-f6f6-f6f6f6f6f6f6"), "ha", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Area unit (10,000 m²)", "Hectare", null, null },
                    { new Guid("f7f7f7f7-f7f7-f7f7-f7f7-f7f7f7f7f7f7"), "s", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Time unit", "Second", null, null },
                    { new Guid("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"), "min", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Time unit (60 seconds)", "Minute", null, null },
                    { new Guid("f9f9f9f9-f9f9-f9f9-f9f9-f9f9f9f9f9f9"), "h", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Time unit (60 minutes)", "Hour", null, null },
                    { new Guid("fafafafa-fafa-fafa-fafa-fafafafafafa"), "d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Time unit (24 hours)", "Day", null, null },
                    { new Guid("fbfbfbfb-fbfb-fbfb-fbfb-fbfbfbfbfbfb"), "pc", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Count unit", "Piece", null, null },
                    { new Guid("fcfcfcfc-fcfc-fcfc-fcfc-fcfcfcfcfcfc"), "dz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "Count unit (12 pieces)", "Dozen", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountNumber",
                table: "Accounts",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Name",
                table: "Accounts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentAccountId",
                table: "Accounts",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Accounts",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Accounts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Accounts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Accounts",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "ActivityLogs",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "ActivityLogs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "ActivityLogs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CompanyID",
                table: "Branches",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Branches",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Branches",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Branches",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Companies",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Companies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Companies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_CompanyID",
                table: "CompanySettingses",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_CurrencyId",
                table: "CompanySettingses",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_DefaultARAccountId",
                table: "CompanySettingses",
                column: "DefaultARAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_DefaultCashAccountId",
                table: "CompanySettingses",
                column: "DefaultCashAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_DefaultDiscountsAccountId",
                table: "CompanySettingses",
                column: "DefaultDiscountsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_DefaultRevenueAccountId",
                table: "CompanySettingses",
                column: "DefaultRevenueAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_DefaultTaxPayableAccountId",
                table: "CompanySettingses",
                column: "DefaultTaxPayableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_PayrollDeductionsAccountId",
                table: "CompanySettingses",
                column: "PayrollDeductionsAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_PayrollPayableAccountId",
                table: "CompanySettingses",
                column: "PayrollPayableAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanySettingses_SalaryExpenseAccountId",
                table: "CompanySettingses",
                column: "SalaryExpenseAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "CompanySettingses",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "CompanySettingses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "CompanySettingses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_ParentId",
                table: "CostCenters",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "CostCenters",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "CostCenters",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "CostCenters",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "CostCenters",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Currencies",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Currencies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Currencies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Customers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Customers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Customers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Customers",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomFinancialReportResults_CompanyID",
                table: "CustomFinancialReportResults",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Inventories",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Inventories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Inventories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductID",
                table: "Inventories",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_WarehouseID",
                table: "Inventories",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Inventories",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "InventoryAdjustments",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "InventoryAdjustments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "InventoryAdjustments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustments_ProductID",
                table: "InventoryAdjustments",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAdjustments_WarehouseID",
                table: "InventoryAdjustments",
                column: "WarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "InventoryAdjustments",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "InventoryPolicies",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "InventoryPolicies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "InventoryPolicies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryPolicies_InventoryID",
                table: "InventoryPolicies",
                column: "InventoryID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "InventoryPolicies",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "InventoryTransactions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "InventoryTransactions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "InventoryTransactions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_InventoryID",
                table: "InventoryTransactions",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransactions_RelatedJournalEntryID",
                table: "InventoryTransactions",
                column: "RelatedJournalEntryID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "InventoryTransactions",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "InvoiceApprovals",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "InvoiceApprovals",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "InvoiceApprovals",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceApprovals_InvoiceId",
                table: "InvoiceApprovals",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "InvoiceApprovals",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "InvoiceDiscounts",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "InvoiceDiscounts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "InvoiceDiscounts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDiscounts_InvoiceId",
                table: "InvoiceDiscounts",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "InvoiceDiscounts",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "InvoiceLines",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "InvoiceLines",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "InvoiceLines",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_ProductId",
                table: "InvoiceLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "InvoiceLines",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Invoices",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Invoices",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Invoices",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CurrencyId",
                table: "Invoices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Invoices",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "InvoiceTaxes",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "InvoiceTaxes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "InvoiceTaxes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTaxes_InvoiceId",
                table: "InvoiceTaxes",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "InvoiceTaxes",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "JournalEntries",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "JournalEntries",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "JournalEntries",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "JournalEntries",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "JournalEntryLines",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "JournalEntryLines",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "JournalEntryLines",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_AccountId",
                table: "JournalEntryLines",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_CostCenterId",
                table: "JournalEntryLines",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_JournalEntryId",
                table: "JournalEntryLines",
                column: "JournalEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "JournalEntryLines",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                schema: "logging",
                table: "LogEntries",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                schema: "logging",
                table: "LogEntries",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_Category",
                schema: "logging",
                table: "LogEntries",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_CorrelationId",
                schema: "logging",
                table: "LogEntries",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_CreatedBy",
                schema: "logging",
                table: "LogEntries",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_Entity",
                schema: "logging",
                table: "LogEntries",
                columns: new[] { "EntityName", "EntityKey" });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_Level",
                schema: "logging",
                table: "LogEntries",
                column: "Level");

            migrationBuilder.CreateIndex(
                name: "IX_LogEntry_Tenant_OccurredAt",
                schema: "logging",
                table: "LogEntries",
                columns: new[] { "TenantID", "OccurredAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                schema: "logging",
                table: "LogEntries",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                schema: "notification",
                table: "Notifications",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                schema: "notification",
                table: "Notifications",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                schema: "notification",
                table: "Notifications",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CorrelationId",
                schema: "notification",
                table: "Notifications",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Recipient",
                schema: "notification",
                table: "Notifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Recipient_Status",
                schema: "notification",
                table: "Notifications",
                columns: new[] { "RecipientId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ScheduledAt",
                schema: "notification",
                table: "Notifications",
                column: "ScheduledAt");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Status",
                schema: "notification",
                table: "Notifications",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                schema: "notification",
                table: "Notifications",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "OrderLines",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "OrderLines",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "OrderLines",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_ProductId",
                table: "OrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "OrderLines",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Orders",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Orders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Orders",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CurrencyId",
                table: "Orders",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Orders",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Payments",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Payments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Payments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Payments",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "ProductBundles",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "ProductBundles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "ProductBundles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundles_ComponentProductId",
                table: "ProductBundles",
                column: "ComponentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundles_ParentProductId",
                table: "ProductBundles",
                column: "ParentProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBundles_UnitOfMeasurementId",
                table: "ProductBundles",
                column: "UnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "ProductBundles",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "ProductCategories",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "ProductCategories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "ProductCategories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ParentCategoryId",
                table: "ProductCategories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_TenantID_Name",
                table: "ProductCategories",
                columns: new[] { "TenantID", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "ProductCategories",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "ProductPrices",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "ProductPrices",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "ProductPrices",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_CurrencyId",
                table: "ProductPrices",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "ProductPrices",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Products",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Products",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Products",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitOfMeasurementId",
                table: "Products",
                column: "UnitOfMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Products",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "ProductTaxes",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "ProductTaxes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "ProductTaxes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTaxes_ProductId",
                table: "ProductTaxes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "ProductTaxes",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "ProductUnitConversions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "ProductUnitConversions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "ProductUnitConversions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnitConversions_FromUnitId",
                table: "ProductUnitConversions",
                column: "FromUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnitConversions_ProductId",
                table: "ProductUnitConversions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnitConversions_ToUnitId",
                table: "ProductUnitConversions",
                column: "ToUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "ProductUnitConversions",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "SecurityAlerts",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "SecurityAlerts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "SecurityAlerts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityAlerts_UserId",
                table: "SecurityAlerts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Sessions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Sessions",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Sessions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "SocialAccounts",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "SocialAccounts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "SocialAccounts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SocialAccounts_UserId",
                table: "SocialAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "StockTransfers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "StockTransfers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "StockTransfers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_FromWarehouseID",
                table: "StockTransfers",
                column: "FromWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_ProductID",
                table: "StockTransfers",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_ToWarehouseID",
                table: "StockTransfers",
                column: "ToWarehouseID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "StockTransfers",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Suppliers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Suppliers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Suppliers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Suppliers",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "TrustedDevices",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "TrustedDevices",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "TrustedDevices",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TrustedDevices_UserId",
                table: "TrustedDevices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "UnitsOfMeasurement",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "UnitsOfMeasurement",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "UnitsOfMeasurement",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Warehouses",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Warehouses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Warehouses",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Warehouses",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_BranchID",
                table: "Warehouses",
                column: "BranchID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompanySettingses");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CustomFinancialReportResults");

            migrationBuilder.DropTable(
                name: "InventoryAdjustments");

            migrationBuilder.DropTable(
                name: "InventoryPolicies");

            migrationBuilder.DropTable(
                name: "InventoryTransactions");

            migrationBuilder.DropTable(
                name: "InvoiceApprovals");

            migrationBuilder.DropTable(
                name: "InvoiceDiscounts");

            migrationBuilder.DropTable(
                name: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "InvoiceTaxes");

            migrationBuilder.DropTable(
                name: "JournalEntryLines");

            migrationBuilder.DropTable(
                name: "LogEntries",
                schema: "logging");

            migrationBuilder.DropTable(
                name: "Notifications",
                schema: "notification");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductBundles");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "ProductTaxes");

            migrationBuilder.DropTable(
                name: "ProductUnitConversions");

            migrationBuilder.DropTable(
                name: "SecurityAlerts");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "SocialAccounts");

            migrationBuilder.DropTable(
                name: "StockTransfers");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "TrustedDevices");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CostCenters");

            migrationBuilder.DropTable(
                name: "JournalEntries");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "UnitsOfMeasurement");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
