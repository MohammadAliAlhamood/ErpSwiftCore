using ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Commands;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryAdjustmentService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesAdjustment.Handlers.CommandsHandler
{
    // 1. Check if an adjustment exists by ID
    public class CheckAdjustmentExistsCommandHandler
        : BaseHandler<CheckAdjustmentExistsCommand>
    {
        private readonly IInventoryAdjustmentValidationService _validation;

        public CheckAdjustmentExistsCommandHandler(
            IInventoryAdjustmentValidationService validation,
            ILogger<BaseHandler<CheckAdjustmentExistsCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckAdjustmentExistsCommand req,
            CancellationToken ct)
        {
            var exists = await _validation.AdjustmentExistsByIdAsync(
                req.Dto.AdjustmentId, ct);
            return new { Exists = exists };
        }
    }

    // 2. Check existence for product & warehouse on date (with optional exclude)
    public class CheckExistsForProductWarehouseOnDateCommandHandler
        : BaseHandler<CheckExistsForProductWarehouseOnDateCommand>
    {
        private readonly IInventoryAdjustmentValidationService _validation;

        public CheckExistsForProductWarehouseOnDateCommandHandler(
            IInventoryAdjustmentValidationService validation,
            ILogger<BaseHandler<CheckExistsForProductWarehouseOnDateCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckExistsForProductWarehouseOnDateCommand req,
            CancellationToken ct)
        {
            var dto = req.Dto;
            var exists = await _validation.ExistsForProductAndWarehouseOnDateAsync(
                dto.ProductId,
                dto.WarehouseId,
                dto.Date,
                dto.ExcludeId,
                ct);
            return new { Exists = exists };
        }
    }

    // 3. Validate product ID
    public class ValidateProductCommandHandler
        : BaseHandler<ValidateProductCommand>
    {
        private readonly IInventoryAdjustmentValidationService _validation;

        public ValidateProductCommandHandler(
            IInventoryAdjustmentValidationService validation,
            ILogger<BaseHandler<ValidateProductCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateProductCommand req,
            CancellationToken ct)
        {
            var isValid = await _validation.IsValidProductAsync(
                req.Dto.ProductId, ct);
            return new { IsValid = isValid };
        }
    }

    // 4. Validate warehouse ID
    public class ValidateWarehouseCommandHandler
        : BaseHandler<ValidateWarehouseCommand>
    {
        private readonly IInventoryAdjustmentValidationService _validation;

        public ValidateWarehouseCommandHandler(
            IInventoryAdjustmentValidationService validation,
            ILogger<BaseHandler<ValidateWarehouseCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateWarehouseCommand req,
            CancellationToken ct)
        {
            var isValid = await _validation.IsValidWarehouseAsync(
                req.Dto.WarehouseId, ct);
            return new { IsValid = isValid };
        }
    }

    // 5. Validate quantity (non-zero)
    public class ValidateQuantityCommandHandler
        : BaseHandler<ValidateQuantityCommand>
    {
        private readonly IInventoryAdjustmentValidationService _validation;

        public ValidateQuantityCommandHandler(
            IInventoryAdjustmentValidationService validation,
            ILogger<BaseHandler<ValidateQuantityCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateQuantityCommand req,
            CancellationToken ct)
        {
            var isValid = await _validation.IsValidQuantityAsync(
                req.Dto.Quantity, ct);
            return new { IsValid = isValid };
        }
    }
}