using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Dtos;
using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Queries;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Handlers.QueriesHandler
{
    #region ──────────── CompanyBranch Query Handlers ────────────

    public class GetBranchByIdQueryHandler : BaseHandler<GetBranchByIdQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public GetBranchByIdQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<GetBranchByIdQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _branchQueryService.GetBranchByIdAsync(request.BranchId, cancellationToken);
            if (entity == null)
                throw new DomainNotFoundException($"الفرع بالمعرّف '{request.BranchId}' غير موجود.");
            return _mapper.Map<CompanyBranchDto>(entity);
        }
    }

    public class GetBranchWithCompanyQueryHandler : BaseHandler<GetBranchWithCompanyQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public GetBranchWithCompanyQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<GetBranchWithCompanyQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetBranchWithCompanyQuery request, CancellationToken cancellationToken)
        {
            var entity = await _branchQueryService.GetBranchWithCompanyAsync(request.BranchId, cancellationToken);
            if (entity == null)
                throw new DomainNotFoundException($"الفرع بالمعرّف '{request.BranchId}' غير موجود.");
            return _mapper.Map<CompanyBranchDto>(entity);
        }
    }

    public class GetAllBranchesQueryHandler : BaseHandler<GetAllBranchesQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public GetAllBranchesQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<GetAllBranchesQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetAllBranchesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _branchQueryService.GetAllBranchesAsync(cancellationToken);
            var dtos = entities.Select(b => _mapper.Map<CompanyBranchDto>(b)).ToList();
            return dtos;
        }
    }

    public class GetBranchesByCompanyIdQueryHandler : BaseHandler<GetBranchesByCompanyIdQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public GetBranchesByCompanyIdQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<GetBranchesByCompanyIdQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetBranchesByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _branchQueryService.GetBranchesByCompanyIdAsync(request.CompanyId, cancellationToken);
            var dtos = entities.Select(b => _mapper.Map<CompanyBranchDto>(b)).ToList();
            return dtos;
        }
    }

  

    public class GetSoftDeletedBranchesByCompanyIdQueryHandler : BaseHandler<GetSoftDeletedBranchesByCompanyIdQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public GetSoftDeletedBranchesByCompanyIdQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<GetSoftDeletedBranchesByCompanyIdQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedBranchesByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _branchQueryService.GetSoftDeletedBranchesByCompanyIdAsync(request.CompanyId, cancellationToken);
            var dtos = entities.Select(b => _mapper.Map<CompanyBranchDto>(b)).ToList();
            return dtos;
        }
    }

    public class GetBranchesPagedQueryHandler : BaseHandler<GetBranchesPagedQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public GetBranchesPagedQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<GetBranchesPagedQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetBranchesPagedQuery request, CancellationToken cancellationToken)
        {
            var (entities, total) = await _branchQueryService.GetBranchesPagedAsync(request.CompanyId, request.PageIndex, request.PageSize, cancellationToken);
            var dtos = entities.Select(b => _mapper.Map<CompanyBranchDto>(b)).ToList();
            return new CompanyBranchPagedResultDto
            {
                Branches = dtos,
                TotalCount = total
            };
        }
    }

    

    public class GetSoftDeletedBranchesPagedQueryHandler : BaseHandler<GetSoftDeletedBranchesPagedQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public GetSoftDeletedBranchesPagedQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<GetSoftDeletedBranchesPagedQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedBranchesPagedQuery request, CancellationToken cancellationToken)
        {
            var (entities, total) = await _branchQueryService.GetSoftDeletedBranchesPagedAsync(request.CompanyId, request.PageIndex, request.PageSize, cancellationToken);
            var dtos = entities.Select(b => _mapper.Map<CompanyBranchDto>(b)).ToList();
            return new CompanyBranchPagedResultDto
            {
                Branches = dtos,
                TotalCount = total
            };
        }
    }

    public class SearchBranchesByNameQueryHandler : BaseHandler<SearchBranchesByNameQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public SearchBranchesByNameQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<SearchBranchesByNameQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(SearchBranchesByNameQuery request, CancellationToken cancellationToken)
        {
            var entities = await _branchQueryService.SearchBranchesByNameAsync(request.CompanyId, request.Name, cancellationToken);
            var dtos = entities.Select(b => _mapper.Map<CompanyBranchDto>(b)).ToList();
            return dtos;
        }
    }

    public class SearchBranchesByCodeQueryHandler : BaseHandler<SearchBranchesByCodeQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public SearchBranchesByCodeQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<SearchBranchesByCodeQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(SearchBranchesByCodeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _branchQueryService.SearchBranchesByCodeAsync(request.CompanyId, request.Code, cancellationToken);
            var dtos = entities.Select(b => _mapper.Map<CompanyBranchDto>(b)).ToList();
            return dtos;
        }
    }

    public class SearchBranchesByKeywordQueryHandler : BaseHandler<SearchBranchesByKeywordQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public SearchBranchesByKeywordQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<SearchBranchesByKeywordQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(SearchBranchesByKeywordQuery request, CancellationToken cancellationToken)
        {
            var entities = await _branchQueryService.SearchBranchesByKeywordAsync(request.CompanyId, request.Keyword, cancellationToken);
            var dtos = entities.Select(b => _mapper.Map<CompanyBranchDto>(b)).ToList();
            return dtos;
        }
    }

    public class BranchExistsQueryHandler : BaseHandler<BranchExistsQuery>
    {
        private readonly ICompanyBranchValidationService _branchValidationService;

        public BranchExistsQueryHandler(
            ICompanyBranchValidationService branchValidationService,
            ILogger<BranchExistsQueryHandler> logger
        ) : base(logger)
        {
            _branchValidationService = branchValidationService;
        }

        protected override async Task<object?> HandleRequestAsync(BranchExistsQuery request, CancellationToken cancellationToken)
        {
            var exists = await _branchValidationService.BranchExistsAsync(request.BranchId, cancellationToken);
            return new { Exists = exists };
        }
    }

    public class BranchExistsByCodeQueryHandler : BaseHandler<BranchExistsByCodeQuery>
    {
        private readonly ICompanyBranchValidationService _branchValidationService;

        public BranchExistsByCodeQueryHandler(
            ICompanyBranchValidationService branchValidationService,
            ILogger<BranchExistsByCodeQueryHandler> logger
        ) : base(logger)
        {
            _branchValidationService = branchValidationService;
        }

        protected override async Task<object?> HandleRequestAsync(BranchExistsByCodeQuery request, CancellationToken cancellationToken)
        {
            var exists = await _branchValidationService.BranchExistsByCodeAsync(request.CompanyId, request.BranchCode, cancellationToken);
            return new { Exists = exists };
        }
    }

    public class IsBranchNameUniqueQueryHandler : BaseHandler<IsBranchNameUniqueQuery>
    {
        private readonly ICompanyBranchValidationService _branchValidationService;

        public IsBranchNameUniqueQueryHandler(
            ICompanyBranchValidationService branchValidationService,
            ILogger<IsBranchNameUniqueQueryHandler> logger
        ) : base(logger)
        {
            _branchValidationService = branchValidationService;
        }

        protected override async Task<object?> HandleRequestAsync(IsBranchNameUniqueQuery request, CancellationToken cancellationToken)
        {
            var isUnique = await _branchValidationService.IsBranchNameUniqueAsync(request.CompanyId, request.BranchName, request.ExcludeBranchId, cancellationToken);
            return new { IsUnique = isUnique };
        }
    }

    public class HasBranchesQueryHandler : BaseHandler<HasBranchesQuery>
    {
        private readonly ICompanyBranchValidationService _branchValidationService;

        public HasBranchesQueryHandler(
            ICompanyBranchValidationService branchValidationService,
            ILogger<HasBranchesQueryHandler> logger
        ) : base(logger)
        {
            _branchValidationService = branchValidationService;
        }

        protected override async Task<object?> HandleRequestAsync(HasBranchesQuery request, CancellationToken cancellationToken)
        {
            var has = await _branchValidationService.HasBranchesAsync(request.CompanyId, cancellationToken);
            return new { HasBranches = has };
        }
    }

    public class GetBranchesCountQueryHandler : BaseHandler<GetBranchesCountQuery>
    {
        private readonly ICompanyBranchQueryService _branchQueryService;

        public GetBranchesCountQueryHandler(
            ICompanyBranchQueryService branchQueryService,
            ILogger<GetBranchesCountQueryHandler> logger
        ) : base(logger)
        {
            _branchQueryService = branchQueryService;
        }

        protected override async Task<object?> HandleRequestAsync(GetBranchesCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _branchQueryService.GetBranchesCountAsync(request.CompanyId, cancellationToken);
            return new { Count = count };
        }
    }

   
    #endregion
}
