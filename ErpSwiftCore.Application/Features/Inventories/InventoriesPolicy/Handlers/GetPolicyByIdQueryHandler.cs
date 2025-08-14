using AutoMapper;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Dtos;
using ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Queries;
using ErpSwiftCore.Domain.IServices.IInventoriesService.IInventoryPolicyService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Handlers
{
    // 1. Get policy by its ID
    public class GetPolicyByIdQueryHandler
        : BaseHandler<GetPolicyByIdQuery>
    {
        private readonly IInventoryPolicyQueryService _svc;
        private readonly IMapper _mapper;

        public GetPolicyByIdQueryHandler(
            IInventoryPolicyQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPolicyByIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPolicyByIdQuery req,
            CancellationToken ct)
        {
            var policy = await _svc.GetPolicyByIdAsync(req.PolicyId, ct);
            return policy is null ? null : _mapper.Map<InventoryPolicyDto>(policy);
        }
    }

    // 2. Get policy by Inventory ID
    public class GetPolicyByInventoryIdQueryHandler
        : BaseHandler<GetPolicyByInventoryIdQuery>
    {
        private readonly IInventoryPolicyQueryService _svc;
        private readonly IMapper _mapper;

        public GetPolicyByInventoryIdQueryHandler(
            IInventoryPolicyQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPolicyByInventoryIdQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPolicyByInventoryIdQuery req,
            CancellationToken ct)
        {
            var policy = await _svc.GetPolicyByInventoryIdAsync(req.InventoryId, ct);
            return policy is null ? null : _mapper.Map<InventoryPolicyDto>(policy);
        }
    }

    // 3. Get all policies
    public class GetAllPoliciesQueryHandler
        : BaseHandler<GetAllPoliciesQuery>
    {
        private readonly IInventoryPolicyQueryService _svc;
        private readonly IMapper _mapper;

        public GetAllPoliciesQueryHandler(
            IInventoryPolicyQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetAllPoliciesQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetAllPoliciesQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetAllPoliciesAsync(ct);
            return list.Select(p => _mapper.Map<InventoryPolicyDto>(p));
        }
    }

    // 4. Get policies by policy type
    public class GetPoliciesByTypeQueryHandler
        : BaseHandler<GetPoliciesByTypeQuery>
    {
        private readonly IInventoryPolicyQueryService _svc;
        private readonly IMapper _mapper;

        public GetPoliciesByTypeQueryHandler(
            IInventoryPolicyQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPoliciesByTypeQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPoliciesByTypeQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetPoliciesByTypeAsync(req.PolicyType, ct);
            return list.Select(p => _mapper.Map<InventoryPolicyDto>(p));
        }
    }

    // 5. Get policies with auto‑reorder enabled
    public class GetPoliciesWithAutoReorderQueryHandler
        : BaseHandler<GetPoliciesWithAutoReorderQuery>
    {
        private readonly IInventoryPolicyQueryService _svc;
        private readonly IMapper _mapper;

        public GetPoliciesWithAutoReorderQueryHandler(
            IInventoryPolicyQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPoliciesWithAutoReorderQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPoliciesWithAutoReorderQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetPoliciesWithAutoReorderAsync(ct);
            return list.Select(p => _mapper.Map<InventoryPolicyDto>(p));
        }
    }

    // 6. Get policies below their reorder level
    public class GetPoliciesBelowReorderLevelQueryHandler
        : BaseHandler<GetPoliciesBelowReorderLevelQuery>
    {
        private readonly IInventoryPolicyQueryService _svc;
        private readonly IMapper _mapper;

        public GetPoliciesBelowReorderLevelQueryHandler(
            IInventoryPolicyQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPoliciesBelowReorderLevelQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPoliciesBelowReorderLevelQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetPoliciesBelowReorderLevelAsync(ct);
            return list.Select(p => _mapper.Map<InventoryPolicyDto>(p));
        }
    }

    // 7. Get policies above their max stock level
    public class GetPoliciesAboveMaxStockLevelQueryHandler
        : BaseHandler<GetPoliciesAboveMaxStockLevelQuery>
    {
        private readonly IInventoryPolicyQueryService _svc;
        private readonly IMapper _mapper;

        public GetPoliciesAboveMaxStockLevelQueryHandler(
            IInventoryPolicyQueryService svc,
            IMapper mapper,
            ILogger<BaseHandler<GetPoliciesAboveMaxStockLevelQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPoliciesAboveMaxStockLevelQuery req,
            CancellationToken ct)
        {
            var list = await _svc.GetPoliciesAboveMaxStockLevelAsync(ct);
            return list.Select(p => _mapper.Map<InventoryPolicyDto>(p));
        }
    }

    // 8. Get total count of policies
    public class GetPoliciesCountQueryHandler
        : BaseHandler<GetPoliciesCountQuery>
    {
        private readonly IInventoryPolicyQueryService _svc;

        public GetPoliciesCountQueryHandler(
            IInventoryPolicyQueryService svc,
            ILogger<BaseHandler<GetPoliciesCountQuery>> logger
        ) : base(logger)
        {
            _svc = svc;
        }

        protected override async Task<object?> HandleRequestAsync(
            GetPoliciesCountQuery req,
            CancellationToken ct)
        {
            return await _svc.GetPoliciesCountAsync(ct);
        }
    }
}







