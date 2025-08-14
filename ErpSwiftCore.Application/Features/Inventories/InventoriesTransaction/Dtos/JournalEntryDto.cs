using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using ErpSwiftCore.Application.Features.Financials.JournalEntry.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Inventories.Dtos;
using ErpSwiftCore.Application.Features.Inventories.Warehouses.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Dtos
{
 
    public class InventoryTransactionDto : AuditableEntityDto
    { 
        public Guid InventoryID { get; set; }  
        public InventoryDto Inventory { get; set; } = null!; 
        public InventoryTransactionType TransactionType { get; set; } 
        public DateTime TransactionDate { get; set; } 
        public int Quantity { get; set; } 
        public int RunningBalance { get; set; } 
        public string? ReferenceNumber { get; set; } 
        public string? Notes { get; set; } 
        public Guid? RelatedJournalEntryID { get; set; } 
        public JournalEntryDto? RelatedJournalEntry { get; set; }
    } 
    public class InventoryTransactionSummaryDto
    { 
        public InventoryTransactionType TransactionType { get; set; }
        public int Count { get; set; }
    }
    public class InventoryTransactionAggregateDto
    {
        public Guid ProductID { get; set; } 
        public int TotalQuantity { get; set; } 
        public decimal TurnoverRate { get; set; }
    } 
     
}