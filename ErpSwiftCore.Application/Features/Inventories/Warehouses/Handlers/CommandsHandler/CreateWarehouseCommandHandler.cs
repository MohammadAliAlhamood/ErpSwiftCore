using AutoMapper;
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
    // 1. Create warehouse
    public class CreateWarehouseCommandHandler
        : BaseHandler<CreateWarehouseCommand>
    {
        private readonly IWarehouseCommandService _svc;
        private readonly IMapper _mapper;

        public CreateWarehouseCommandHandler(
            IWarehouseCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<CreateWarehouseCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            CreateWarehouseCommand req,
            CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.EntityInventory.Warehouse>(req.Dto);
            var id = await _svc.CreateWarehouseAsync(entity, ct);
            return new { Id = id };
        }
    }

    // 2. Update warehouse
    public class UpdateWarehouseCommandHandler
        : BaseHandler<UpdateWarehouseCommand>
    {
        private readonly IWarehouseCommandService _svc;
        private readonly IMapper _mapper;

        public UpdateWarehouseCommandHandler(
            IWarehouseCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdateWarehouseCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateWarehouseCommand req,
            CancellationToken ct)
        {
            var entity = _mapper.Map<Domain.Entities.EntityInventory.Warehouse>(req.Dto);
            var ok = await _svc.UpdateWarehouseAsync(entity, ct);
            return new { Success = ok };
        }
    }

    // 3. Delete single warehouse
    public class DeleteWarehouseCommandHandler
        : BaseHandler<DeleteWarehouseCommand>
    {
        private readonly IWarehouseCommandService _svc;

        public DeleteWarehouseCommandHandler(
            IWarehouseCommandService svc,
            ILogger<BaseHandler<DeleteWarehouseCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteWarehouseCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteWarehouseAsync(req.Dto.WarehouseId, ct);
            return new { Success = ok };
        }
    }

    // 4. Delete range of warehouses
    public class DeleteWarehousesRangeCommandHandler
        : BaseHandler<DeleteWarehousesRangeCommand>
    {
        private readonly IWarehouseCommandService _svc;

        public DeleteWarehousesRangeCommandHandler(
            IWarehouseCommandService svc,
            ILogger<BaseHandler<DeleteWarehousesRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteWarehousesRangeCommand req,
            CancellationToken ct)
        {
            var count = await _svc.DeleteWarehousesRangeAsync(req.Dto.WarehouseIds, ct);
            return new { DeletedCount = count };
        }
    }

    // 5. Delete all warehouses
    public class DeleteAllWarehousesCommandHandler
        : BaseHandler<DeleteAllWarehousesCommand>
    {
        private readonly IWarehouseCommandService _svc;

        public DeleteAllWarehousesCommandHandler(
            IWarehouseCommandService svc,
            ILogger<BaseHandler<DeleteAllWarehousesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DeleteAllWarehousesCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DeleteAllWarehousesAsync(ct);
            return new { Success = ok };
        }
    }

    // 6. Restore single warehouse
    public class RestoreWarehouseCommandHandler
        : BaseHandler<RestoreWarehouseCommand>
    {
        private readonly IWarehouseCommandService _svc;

        public RestoreWarehouseCommandHandler(
            IWarehouseCommandService svc,
            ILogger<BaseHandler<RestoreWarehouseCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreWarehouseCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreWarehouseAsync(req.Dto.WarehouseId, ct);
            return new { Success = ok };
        }
    }

    // 7. Restore range of warehouses
    public class RestoreWarehousesRangeCommandHandler
        : BaseHandler<RestoreWarehousesRangeCommand>
    {
        private readonly IWarehouseCommandService _svc;

        public RestoreWarehousesRangeCommandHandler(
            IWarehouseCommandService svc,
            ILogger<BaseHandler<RestoreWarehousesRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreWarehousesRangeCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreWarehousesRangeAsync(req.Dto.WarehouseIds, ct);
            return new { Success = ok };
        }
    }

    // 8. Restore all warehouses
    public class RestoreAllWarehousesCommandHandler
        : BaseHandler<RestoreAllWarehousesCommand>
    {
        private readonly IWarehouseCommandService _svc;

        public RestoreAllWarehousesCommandHandler(
            IWarehouseCommandService svc,
            ILogger<BaseHandler<RestoreAllWarehousesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            RestoreAllWarehousesCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.RestoreAllWarehousesAsync(ct);
            return new { Success = ok };
        }
    }

    // 9. Add range of warehouses
    public class AddWarehousesRangeCommandHandler
        : BaseHandler<AddWarehousesRangeCommand>
    {
        private readonly IWarehouseCommandService _svc;
        private readonly IMapper _mapper;

        public AddWarehousesRangeCommandHandler(
            IWarehouseCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<AddWarehousesRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            AddWarehousesRangeCommand req,
            CancellationToken ct)
        {
            var entities = req.Dto.Warehouses
                .Select(dto => _mapper.Map<Domain.Entities.EntityInventory.Warehouse>(dto));
            var ids = await _svc.AddWarehousesRangeAsync(entities, ct);
            return new { CreatedIds = ids };
        }
    }

    // 10. Bulk import warehouses
    public class BulkImportWarehousesCommandHandler
        : BaseHandler<BulkImportWarehousesCommand>
    {
        private readonly IWarehouseCommandService _svc;
        private readonly IMapper _mapper;

        public BulkImportWarehousesCommandHandler(
            IWarehouseCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<BulkImportWarehousesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkImportWarehousesCommand req,
            CancellationToken ct)
        {
            var entities = req.Dto.Warehouses
                .Select(dto => _mapper.Map<Domain.Entities.EntityInventory.Warehouse>(dto));
            var count = await _svc.BulkImportWarehousesAsync(entities, ct);
            return new { ImportedCount = count };
        }
    }

    // 11. Bulk delete warehouses
    public class BulkDeleteWarehousesCommandHandler
        : BaseHandler<BulkDeleteWarehousesCommand>
    {
        private readonly IWarehouseCommandService _svc;

        public BulkDeleteWarehousesCommandHandler(
            IWarehouseCommandService svc,
            ILogger<BaseHandler<BulkDeleteWarehousesCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            BulkDeleteWarehousesCommand req,
            CancellationToken ct)
        {
            var count = await _svc.BulkDeleteWarehousesAsync(req.Dto.WarehouseIds, ct);
            return new { DeletedCount = count };
        }
    }
} 