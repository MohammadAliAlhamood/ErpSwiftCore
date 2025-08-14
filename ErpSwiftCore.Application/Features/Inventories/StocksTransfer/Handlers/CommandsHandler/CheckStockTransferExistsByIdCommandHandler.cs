using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Commands;
using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Dtos;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService;
using Microsoft.Extensions.Logging; 
namespace ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Handlers.CommandsHandler
{
    // 1. Check if a stock transfer exists by ID
    public class CheckStockTransferExistsByIdCommandHandler
        : BaseHandler<CheckStockTransferExistsByIdCommand>
    {
        private readonly IStockTransferValidationService _svc;

        public CheckStockTransferExistsByIdCommandHandler(
            IStockTransferValidationService svc,
            ILogger<BaseHandler<CheckStockTransferExistsByIdCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckStockTransferExistsByIdCommand req,
            CancellationToken ct)
        {
            var exists = await _svc.StockTransferExistsByIdAsync(req.TransferId, ct);
            return new StockTransferExistsDto
            {
                TransferId = req.TransferId,
                Exists = exists
            };
        }
    }

    // 2. Check for duplicate transfer
    public class CheckDuplicateTransferCommandHandler
        : BaseHandler<CheckDuplicateTransferCommand>
    {
        private readonly IStockTransferValidationService _svc;

        public CheckDuplicateTransferCommandHandler(
            IStockTransferValidationService svc,
            ILogger<BaseHandler<CheckDuplicateTransferCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckDuplicateTransferCommand req,
            CancellationToken ct)
        {
            var dto = req.Dto;
            var isDup = await _svc.ExistsDuplicateTransferAsync(
                dto.ProductId,
                dto.FromWarehouseId,
                dto.ToWarehouseId,
                dto.TransferDate,
                dto.ExcludeId,
                ct);

            dto.IsDuplicate = isDup;
            return dto;
        }
    }

    // 3. Validate product existence
    public class ValidateProductCommandHandler
        : BaseHandler<ValidateProductCommand>
    {
        private readonly IStockTransferValidationService _svc;

        public ValidateProductCommandHandler(
            IStockTransferValidationService svc,
            ILogger<BaseHandler<ValidateProductCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateProductCommand req,
            CancellationToken ct)
        {
            var valid = await _svc.IsValidProductAsync(req.ProductId, ct);
            return new ProductValidationDto
            {
                ProductId = req.ProductId,
                IsValid = valid
            };
        }
    }

    // 4. Validate warehouse existence
    public class ValidateWarehouseCommandHandler
        : BaseHandler<ValidateWarehouseCommand>
    {
        private readonly IStockTransferValidationService _svc;

        public ValidateWarehouseCommandHandler(
            IStockTransferValidationService svc,
            ILogger<BaseHandler<ValidateWarehouseCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateWarehouseCommand req,
            CancellationToken ct)
        {
            var valid = await _svc.IsValidWarehouseAsync(req.WarehouseId, ct);
            return new WarehouseValidationDto
            {
                WarehouseId = req.WarehouseId,
                IsValid = valid
            };
        }
    }

    // 5. Validate transfer quantity
    public class ValidateQuantityCommandHandler
        : BaseHandler<ValidateQuantityCommand>
    {
        private readonly IStockTransferValidationService _svc;

        public ValidateQuantityCommandHandler(
            IStockTransferValidationService svc,
            ILogger<BaseHandler<ValidateQuantityCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateQuantityCommand req,
            CancellationToken ct)
        {
            var valid = await _svc.IsValidQuantityAsync(req.Quantity, ct);
            return new QuantityValidationDto
            {
                Quantity = req.Quantity,
                IsValid = valid
            };
        }
    }
}