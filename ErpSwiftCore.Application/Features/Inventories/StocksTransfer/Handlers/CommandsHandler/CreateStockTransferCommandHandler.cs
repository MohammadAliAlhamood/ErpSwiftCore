using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Commands;
using ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Dtos;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IStockTransferService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.StocksTransfer.Handlers.CommandsHandler
{
    // 1. Create a new stock transfer
    public class CreateStockTransferCommandHandler
        : BaseHandler<CreateStockTransferCommand>
    {
        private readonly IStockTransferCommandService _svc;
        private readonly IMapper _mapper;

        public CreateStockTransferCommandHandler(
            IStockTransferCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<CreateStockTransferCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            CreateStockTransferCommand req,
            CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.EntityInventory.StockTransfer>(req.Dto);
            var id = await _svc.CreateStockTransferAsync(entity, ct);
            return new CreateStockTransferResultDto { ID = id };
        }
    }

    // 2. Update an existing stock transfer
    public class UpdateStockTransferCommandHandler
        : BaseHandler<UpdateStockTransferCommand>
    {
        private readonly IStockTransferCommandService _svc;
        private readonly IMapper _mapper;

        public UpdateStockTransferCommandHandler(
            IStockTransferCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdateStockTransferCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateStockTransferCommand req,
            CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.EntityInventory.StockTransfer>(req.Dto);
            var ok = await _svc.UpdateStockTransferAsync(entity, ct);
            return new { Success = ok };
        }
    }

    // 3. Delete a stock transfer
    public class DeleteStockTransferCommandHandler
        : BaseHandler<DeleteStockTransferCommand>
    {
        private readonly IStockTransferCommandService _svc;

        public DeleteStockTransferCommandHandler(
            IStockTransferCommandService svc,
            ILogger<BaseHandler<DeleteStockTransferCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteStockTransferCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteStockTransferAsync(req.Dto.ID, ct);
            return new { Success = ok };
        }
    }

    // 4. Delete a range of stock transfers
    public class DeleteStockTransfersRangeCommandHandler
        : BaseHandler<DeleteStockTransfersRangeCommand>
    {
        private readonly IStockTransferCommandService _svc;

        public DeleteStockTransfersRangeCommandHandler(
            IStockTransferCommandService svc,
            ILogger<BaseHandler<DeleteStockTransfersRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteStockTransfersRangeCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteStockTransfersRangeAsync(req.Dto.IDs, ct);
            return new { Success = ok };
        }
    }

    // 5. Delete all stock transfers
    public class DeleteAllStockTransfersCommandHandler
        : BaseHandler<DeleteAllStockTransfersCommand>
    {
        private readonly IStockTransferCommandService _svc;

        public DeleteAllStockTransfersCommandHandler(
            IStockTransferCommandService svc,
            ILogger<BaseHandler<DeleteAllStockTransfersCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteAllStockTransfersCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAllStockTransfersAsync(ct);
            return new { Success = ok };
        }
    }

    // 6. Restore a deleted stock transfer
    public class RestoreStockTransferCommandHandler
        : BaseHandler<RestoreStockTransferCommand>
    {
        private readonly IStockTransferCommandService _svc;

        public RestoreStockTransferCommandHandler(
            IStockTransferCommandService svc,
            ILogger<BaseHandler<RestoreStockTransferCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreStockTransferCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreStockTransferAsync(req.Dto.ID, ct);
            return new { Success = ok };
        }
    }

    // 7. Restore a range of deleted stock transfers
    public class RestoreStockTransfersRangeCommandHandler
        : BaseHandler<RestoreStockTransfersRangeCommand>
    {
        private readonly IStockTransferCommandService _svc;

        public RestoreStockTransfersRangeCommandHandler(
            IStockTransferCommandService svc,
            ILogger<BaseHandler<RestoreStockTransfersRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreStockTransfersRangeCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreStockTransfersRangeAsync(req.Dto.IDs, ct);
            return new { Success = ok };
        }
    }

    // 8. Restore all deleted stock transfers
    public class RestoreAllStockTransfersCommandHandler
        : BaseHandler<RestoreAllStockTransfersCommand>
    {
        private readonly IStockTransferCommandService _svc;

        public RestoreAllStockTransfersCommandHandler(
            IStockTransferCommandService svc,
            ILogger<BaseHandler<RestoreAllStockTransfersCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreAllStockTransfersCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreAllStockTransfersAsync(ct);
            return new { Success = ok };
        }
    }

    // 9. Bulk import stock transfers
    public class BulkImportStockTransfersCommandHandler
        : BaseHandler<BulkImportStockTransfersCommand>
    {
        private readonly IStockTransferCommandService _svc;
        private readonly IMapper _mapper;

        public BulkImportStockTransfersCommandHandler(
            IStockTransferCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<BulkImportStockTransfersCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkImportStockTransfersCommand req,
            CancellationToken ct)
        {
            var entities = req.Dto.Transfers
                .Select(dto => _mapper.Map<Domain.Entities.EntityInventory.StockTransfer>(dto));
            var count = await _svc.BulkImportStockTransfersAsync(entities, ct);
            return new BulkImportResultDto { ImportedCount = count };
        }
    }

    // 10. Bulk delete stock transfers
    public class BulkDeleteStockTransfersCommandHandler
        : BaseHandler<BulkDeleteStockTransfersCommand>
    {
        private readonly IStockTransferCommandService _svc;

        public BulkDeleteStockTransfersCommandHandler(
            IStockTransferCommandService svc,
            ILogger<BaseHandler<BulkDeleteStockTransfersCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkDeleteStockTransfersCommand req,
            CancellationToken ct)
        {
            var count = await _svc.BulkDeleteStockTransfersAsync(req.Dto.IDs, ct);
            return new BulkDeleteResultDto { DeletedCount = count };
        }
    }
}