using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ErpSwiftCore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newEditningefwf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_ParentAccount",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CostCenters_CostCenters_ParentId",
                table: "CostCenters");

            migrationBuilder.DropIndex(
                name: "IX_CostCenters_ParentId",
                table: "CostCenters");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ParentAccountId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PartyType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "CostCenters");

            migrationBuilder.DropColumn(
                name: "ParentAccountId",
                table: "Accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "PartyID",
                table: "Invoices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Parties_Companies_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parties_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parties_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PartyId",
                table: "Orders",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PartyID",
                table: "Invoices",
                column: "PartyID");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedAt",
                table: "Parties",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_CreatedBy",
                table: "Parties",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_IsDeleted",
                table: "Parties",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_CustomerId",
                table: "Parties",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_SupplierId",
                table: "Parties",
                column: "SupplierId",
                unique: true,
                filter: "[SupplierId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TenantID",
                table: "Parties",
                column: "TenantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Parties_PartyID",
                table: "Invoices",
                column: "PartyID",
                principalTable: "Parties",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Parties_PartyId",
                table: "Orders",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Parties_PartyID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Parties_PartyId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PartyId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PartyID",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PartyID",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "PartyType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "CostCenters",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentAccountId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostCenters_ParentId",
                table: "CostCenters",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentAccountId",
                table: "Accounts",
                column: "ParentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_ParentAccount",
                table: "Accounts",
                column: "ParentAccountId",
                principalTable: "Accounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CostCenters_CostCenters_ParentId",
                table: "CostCenters",
                column: "ParentId",
                principalTable: "CostCenters",
                principalColumn: "ID");
        }
    }
}
