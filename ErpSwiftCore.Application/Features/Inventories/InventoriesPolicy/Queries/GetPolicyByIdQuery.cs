using ErpSwiftCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Inventories.InventoriesPolicy.Queries
{
    // 1. Get policy by its ID
    public class GetPolicyByIdQuery : IRequest<APIResponseDto>
    {
        public Guid PolicyId { get; }
        public GetPolicyByIdQuery(Guid policyId) => PolicyId = policyId;
    }

    // 2. Get policy by Inventory ID
    public class GetPolicyByInventoryIdQuery : IRequest<APIResponseDto>
    {
        public Guid InventoryId { get; }
        public GetPolicyByInventoryIdQuery(Guid inventoryId) => InventoryId = inventoryId;
    }

    // 3. Get all policies
    public class GetAllPoliciesQuery : IRequest<APIResponseDto> { }

    // 4. Get policies by policy type
    public class GetPoliciesByTypeQuery : IRequest<APIResponseDto>
    {
        public InventoryPolicyType PolicyType { get; }
        public GetPoliciesByTypeQuery(InventoryPolicyType policyType) => PolicyType = policyType;
    }

    // 5. Get policies with auto‐reorder enabled
    public class GetPoliciesWithAutoReorderQuery : IRequest<APIResponseDto> { }

    // 6. Get policies below their reorder level
    public class GetPoliciesBelowReorderLevelQuery : IRequest<APIResponseDto> { }

    // 7. Get policies above their max stock level
    public class GetPoliciesAboveMaxStockLevelQuery : IRequest<APIResponseDto> { }

    // 8. Get total count of policies
    public class GetPoliciesCountQuery : IRequest<APIResponseDto> { }
}