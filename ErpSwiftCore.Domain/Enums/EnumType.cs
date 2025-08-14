namespace ErpSwiftCore.Domain.Enums
{
    public enum ActivityTargetType { Lead = 1, Customer = 2, Supplier = 3, Opportunity = 4, Quotation = 5 }
    public enum OrderType { Sales, Purchase }
    public enum OrderStatus { Draft, Confirmed, Approved, Rejected, Cancelled, Completed }
    public enum PartyType
    {
        Customer,
        Supplier 
    }
    public enum LeaveType { Unknown = 0, Annual = 1, Sick = 2, Maternity = 3, Paternity = 4, Unpaid = 5, Bereavement = 6, Study = 7, Emergency = 8, Other = 99 }
    public enum PayrollStatus { Pending, Paid, Failed, Reversed }
    public enum PayrollType { Monthly, FinalSettlement, Advance, Bonus }
    public enum PayrollComponentType { Allowance, Deduction, Tax, Insurance, Bonus, Overtime, Other }
    public enum ContractType { Permanent = 1, Temporary = 2, Internship = 3, Consultant = 4, }
    public enum GenderType { Male, Female, Other }
    public enum InvoiceStatus { Draft = 0, Issued = 1, Paid = 2, PartiallyPaid = 3, Overdue = 4, Cancelled = 5 }
    public enum InvoiceType { Unknown = 0, Sales = 1, Purchase = 2, CreditNote = 3, DebitNote = 4, Proforma = 5 }
    public enum LoanStatus { Pending, Approved, Rejected, Cancelled, Paid }
    public enum Periodicity { Unknown = 0, Monthly = 1, Quarterly = 2, SemiAnnual = 3, Annual = 4 }
    public enum LeaveStatus { Pending = 0, Approved = 1, Rejected = 2, Cancelled = 3 }
    public enum InvoiceApprovalStatus { Pending = 0, Approved = 1, Rejected = 2 }
    public enum InventoryTransactionType { Unknown = 0, Purchase = 1, Sale = 2, TransferIn = 3, TransferOut = 4, AdjustmentIncrease = 5, AdjustmentDecrease = 6, ReturnToSupplier = 7, ReturnFromCustomer = 8, OpeningBalance = 9 }
    public enum ProductType { Unknown = 0, FinishedGoods = 1, RawMaterials = 2, Services = 3, Consumables = 4, Kits = 5 }
    public enum SalesOrderStatus { Draft = 0, Submitted = 1, Approved = 2, Rejected = 3, PartiallyShipped = 4, FullyShipped = 5, Cancelled = 6 }
    public enum PurchaseOrderStatus { Draft = 0, Submitted = 1, Approved = 2, Rejected = 3, PartiallyReceived = 4, FullyReceived = 5, Cancelled = 6 }
    public enum InventoryHistoryType { Unknown = 0, ManualAdjustment = 1, StockTransfer = 2, Sale = 3, Purchase = 4, InventoryCount = 5, SystemCorrection = 6 }
    public enum InventoryNotificationType { LowStock = 1, Overstock = 2, AutoReorder = 3, ManualAlert = 4 }
    public enum InventoryPolicyType { FIFO = 1, LIFO = 2, WeightedAverage = 3 }
    public enum RecurringInterval { None = 0, Daily = 1, Weekly = 2, BiWeekly = 3, Monthly = 4, Quarterly = 5, Yearly = 6 }
    public enum TransactionStatus { Pending = 0, Approved = 1, Rejected = 2, Posted = 3, Cancelled = 4, Unknown = 5 }
    public enum TransactionType { Unknown = 0, Asset = 1, Liability = 2, Equity = 3, Revenue = 4, Expense = 5, OtherIncome = 6, OtherExpense = 7, ContraAsset = 8, ContraLiability = 9, OwnerDraw = 10, Capital = 11 }
    public enum AccountType { General = 0, BankAccount = 1, CashAccount = 2, ReceivableAccount = 3, PayableAccount = 4, ExpenseAccount = 5, RevenueAccount = 6, ProvisionAccount = 7, ProfitAndLossAccount = 8, CapitalAccount = 9, ShortTermLoanAccount = 10, LongTermLoanAccount = 11, TaxPayableAccount = 12, TaxReceivableAccount = 13, DepreciationAccount = 14, FixedAssetAccount = 15, InventoryAccount = 16, DiscountAccount = 17, InterbranchTransferAccount = 18, RetainedEarningsAccount = 19, PartnerCurrentAccount = 20, CommissionAccount = 21, DividendAccount = 22, BankChargesAccount = 23, CurrencyRevaluationAccount = 24, SecurityDepositAccount = 25, PrepaidExpenseAccount = 26, DeferredRevenueAccount = 27, AccruedExpenseAccount = 28, OtherAssetAccount = 29, OtherLiabilityAccount = 30 }
    public enum ProductPriceType { Retail = 1, Wholesale = 2, Cost = 3, Contract = 4, Promotional = 5, Special = 6, Draft = 7, Suggested = 8, InternalTransfer = 9, LogisticsCost = 10 }
    public enum LeadStatus { New = 0, Qualified = 1, Disqualified = 2 }
    public enum OpportunityStage { Qualification = 0, Proposal = 1, Negotiation = 2, Won = 3, Lost = 4 }
    public enum ActivityType { Call = 0, Meeting = 1, Email = 2, Task = 3, Other = 4 }
    public enum ActivityStatus { Planned = 0, Completed = 1, Canceled = 2 }
    public enum QuotationStatus { Draft = 0, Sent = 1, Accepted = 2, Rejected = 3 }
}