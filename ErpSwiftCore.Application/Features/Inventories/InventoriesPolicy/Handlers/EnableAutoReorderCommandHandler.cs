using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Commands;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryPolicyService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Handlers
{
    // 1. Enable Auto-Reorder
    public class EnableAutoReorderCommandHandler
        : BaseHandler<EnableAutoReorderCommand>
    {
        private readonly IInventoryPolicyCommandService _svc;

        public EnableAutoReorderCommandHandler(
            IInventoryPolicyCommandService svc,
            ILogger<BaseHandler<EnableAutoReorderCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            EnableAutoReorderCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.EnableAutoReorderAsync(
                req.Dto.InventoryId,
                req.Dto.ReorderQuantity,
                ct);
            return new { Success = ok };
        }
    }

    // 2. Disable Auto-Reorder
    public class DisableAutoReorderCommandHandler
        : BaseHandler<DisableAutoReorderCommand>
    {
        private readonly IInventoryPolicyCommandService _svc;

        public DisableAutoReorderCommandHandler(
            IInventoryPolicyCommandService svc,
            ILogger<BaseHandler<DisableAutoReorderCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            DisableAutoReorderCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.DisableAutoReorderAsync(
                req.Dto.InventoryId,
                ct);
            return new { Success = ok };
        }
    }

    // 3. Update entire policy
    public class UpdatePolicyCommandHandler
        : BaseHandler<UpdatePolicyCommand>
    {
        private readonly IInventoryPolicyCommandService _svc;
        private readonly IMapper _mapper;

        public UpdatePolicyCommandHandler(
            IInventoryPolicyCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdatePolicyCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdatePolicyCommand req,
            CancellationToken ct)
        {
            var policy = _mapper.Map<Domain.AggregateRoots.AggregateInventoryRoots.InventoryPolicy>(req.Dto);
            var ok = await _svc.UpdatePolicyAsync(policy, ct);
            return new { Success = ok };
        }
    }

    // 4. Update reorder level only
    public class UpdateReorderLevelCommandHandler
        : BaseHandler<UpdateReorderLevelCommand>
    {
        private readonly IInventoryPolicyCommandService _svc;

        public UpdateReorderLevelCommandHandler(
            IInventoryPolicyCommandService svc,
            ILogger<BaseHandler<UpdateReorderLevelCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateReorderLevelCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.UpdateReorderLevelAsync(
                req.Dto.PolicyId,
                req.Dto.ReorderLevel,
                ct);
            return new { Success = ok };
        }
    }

    // 5. Update max stock level only
    public class UpdateMaxStockLevelCommandHandler
        : BaseHandler<UpdateMaxStockLevelCommand>
    {
        private readonly IInventoryPolicyCommandService _svc;

        public UpdateMaxStockLevelCommandHandler(
            IInventoryPolicyCommandService svc,
            ILogger<BaseHandler<UpdateMaxStockLevelCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdateMaxStockLevelCommand req,
            CancellationToken ct)
        {
            var ok = await _svc.UpdateMaxStockLevelAsync(
                req.Dto.PolicyId,
                req.Dto.MaxStockLevel,
                ct);
            return new { Success = ok };
        }
    }

    // 6. Update multiple policies at once
    public class UpdatePoliciesRangeCommandHandler
        : BaseHandler<UpdatePoliciesRangeCommand>
    {
        private readonly IInventoryPolicyCommandService _svc;
        private readonly IMapper _mapper;

        public UpdatePoliciesRangeCommandHandler(
            IInventoryPolicyCommandService svc,
            IMapper mapper,
            ILogger<BaseHandler<UpdatePoliciesRangeCommand>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            UpdatePoliciesRangeCommand req,
            CancellationToken ct)
        {
            var entities = req.Dto.Policies
                .Select(dto => _mapper.Map<Domain.AggregateRoots.AggregateInventoryRoots.InventoryPolicy>(dto));
            var ok = await _svc.UpdatePoliciesAsync(entities, ct);
            return new { Success = ok };
        }
    }
} 
