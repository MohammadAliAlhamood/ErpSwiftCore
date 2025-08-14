using ErpSwiftCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesTransaction.Queries
{
    // 1. Get single transaction by its ID
    public class GetTransactionByIdQuery : IRequest<APIResponseDto>
    {
        public Guid TransactionId { get; }

        public GetTransactionByIdQuery(Guid transactionId)
            => TransactionId = transactionId;
    }

    // 2. Get first transaction for a given inventory
    public class GetFirstTransactionForInventoryQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }

        public GetFirstTransactionForInventoryQuery(Guid inventoryId)
            => InventoryId = inventoryId;
    }

    // 3. Get last transaction for a given inventory
    public class GetLastTransactionForInventoryQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }

        public GetLastTransactionForInventoryQuery(Guid inventoryId)
            => InventoryId = inventoryId;
    }

    // 4. Get all transactions for a given inventory
    public class GetTransactionsByInventoryIdQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }

        public GetTransactionsByInventoryIdQuery(Guid inventoryId)
            => InventoryId = inventoryId;
    }

    // 5. Get all transactions for a given product
    public class GetTransactionsByProductIdQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }

        public GetTransactionsByProductIdQuery(Guid productId)
            => ProductId = productId;
    }

    // 6. Get all transactions for a given warehouse
    public class GetTransactionsByWarehouseIdQuery : IRequest<APIResponseDto>
    {
        public Guid WarehouseId { get; }

        public GetTransactionsByWarehouseIdQuery(Guid warehouseId)
            => WarehouseId = warehouseId;
    }

    // 7. Get all transactions of a specific type
    public class GetTransactionsByTypeQuery : IRequest<APIResponseDto>
    {
        public InventoryTransactionType TransactionType { get; }

        public GetTransactionsByTypeQuery(InventoryTransactionType transactionType)
            => TransactionType = transactionType;
    }

    // 8. Get all transactions in a date range
    public class GetTransactionsByDateRangeQuery : IRequest<APIResponseDto>
    {
        public DateTime From { get; }
        public DateTime To { get; }

        public GetTransactionsByDateRangeQuery(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
    }

    // 9. Get all transactions affecting the available balance for an inventory
    public class GetTransactionsAffectingBalanceQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }

        public GetTransactionsAffectingBalanceQuery(Guid inventoryId)
            => InventoryId = inventoryId;
    }

    // 10. Search transactions whose notes contain a given term
    public class SearchTransactionsByNotesQuery : IRequest<APIResponseDto>
    {
        public string NoteTerm { get; }

        public SearchTransactionsByNotesQuery(string noteTerm)
            => NoteTerm = noteTerm;
    }

    // 11. Get total count of all transactions
    public class GetTransactionsCountQuery : IRequest<APIResponseDto>
    {
    }

    // 12. Sum quantity moved for a product in a date range
    public class SumQuantityByProductAndDateRangeQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public DateTime From { get; }
        public DateTime To { get; }

        public SumQuantityByProductAndDateRangeQuery(Guid productId, DateTime from, DateTime to)
        {
            ProductId = productId;
            From = from;
            To = to;
        }
    }

    // 13. Calculate turnover rate for a product in a date range
    public class GetTurnoverRateQuery : IRequest<APIResponseDto>
    {
        public Guid ProductId { get; }
        public DateTime From { get; }
        public DateTime To { get; }

        public GetTurnoverRateQuery(Guid productId, DateTime from, DateTime to)
        {
            ProductId = productId;
            From = from;
            To = to;
        }
    }
}



 