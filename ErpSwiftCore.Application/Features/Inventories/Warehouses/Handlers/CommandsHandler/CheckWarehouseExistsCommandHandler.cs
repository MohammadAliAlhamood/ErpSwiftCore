using ErpSwiftCore.Application.Features.Inventories.Warehouses.Commands;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IWarehouseService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.Warehouses.Handlers.CommandsHandler
{
    /// <summary>
    /// Handler لفحص وجود مستودع بمعرّف معين
    /// </summary>
    public class CheckWarehouseExistsCommandHandler
        : BaseHandler<CheckWarehouseExistsCommand>
    {
        private readonly IWarehouseValidationService _validation;

        public CheckWarehouseExistsCommandHandler(
            IWarehouseValidationService validation,
            ILogger<BaseHandler<CheckWarehouseExistsCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckWarehouseExistsCommand req,
            CancellationToken ct)
        {
            var exists = await _validation.WarehouseExistsByIdAsync(
                req.Dto.WarehouseId, ct);
            return new { Exists = exists };
        }
    }

    /// <summary>
    /// Handler لفحص تكرار اسم مستودع في فرع ما
    /// </summary>
    public class CheckExistsWithNameCommandHandler
        : BaseHandler<CheckExistsWithNameCommand>
    {
        private readonly IWarehouseValidationService _validation;

        public CheckExistsWithNameCommandHandler(
            IWarehouseValidationService validation,
            ILogger<BaseHandler<CheckExistsWithNameCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            CheckExistsWithNameCommand req,
            CancellationToken ct)
        {
            var dto = req.Dto;
            var exists = await _validation.ExistsWithNameAsync(
                dto.Name, dto.BranchId, dto.ExcludeId, ct);
            return new { Exists = exists };
        }
    }

    /// <summary>
    /// Handler لفحص صلاحية الفرع المرتبط بالمستودع
    /// </summary>
    public class ValidateBranchCommandHandler
        : BaseHandler<ValidateBranchCommand>
    {
        private readonly IWarehouseValidationService _validation;

        public ValidateBranchCommandHandler(
            IWarehouseValidationService validation,
            ILogger<BaseHandler<ValidateBranchCommand>> logger
        ) : base(logger)
        {
            _validation = validation;
        }

        protected override async Task<object?> HandleRequestAsync(
            ValidateBranchCommand req,
            CancellationToken ct)
        {
            var isValid = await _validation.IsValidBranchAsync(
                req.Dto.BranchId, ct);
            return new { IsValid = isValid };
        }
    }
}