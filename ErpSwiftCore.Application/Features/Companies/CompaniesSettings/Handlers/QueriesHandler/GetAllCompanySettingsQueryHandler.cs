using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Queries;
using ErpSwiftCore.Domain.IServices.ICompanyServices;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Handlers.QueriesHandler
{
    #region ──────────── CompanySettings Query Handlers ────────────

    public class GetAllCompanySettingsQueryHandler : BaseHandler<GetAllCompanySettingsQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public GetAllCompanySettingsQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<GetAllCompanySettingsQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetAllCompanySettingsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _settingsQueryService.GetAllCompanySettingsAsync(cancellationToken);
            var dtos = entities.Select(s => _mapper.Map<CompanySettingsDto>(s)).ToList();
            return dtos;
        }
    }

 

    public class GetSoftDeletedCompanySettingsQueryHandler : BaseHandler<GetSoftDeletedCompanySettingsQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public GetSoftDeletedCompanySettingsQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<GetSoftDeletedCompanySettingsQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetSoftDeletedCompanySettingsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _settingsQueryService.GetSoftDeletedCompanySettingsAsync(cancellationToken);
            var dtos = entities.Select(s => _mapper.Map<CompanySettingsDto>(s)).ToList();
            return dtos;
        }
    }

    public class GetCompanySettingsByCompanyIdQueryHandler : BaseHandler<GetCompanySettingsByCompanyIdQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public GetCompanySettingsByCompanyIdQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<GetCompanySettingsByCompanyIdQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompanySettingsByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _settingsQueryService.GetCompanySettingsByCompanyIdAsync(request.CompanyId, cancellationToken);
            if (entity == null)
                throw new DomainNotFoundException($"إعدادات الشركة '{request.CompanyId}' غير موجودة.");
            return _mapper.Map<CompanySettingsDto>(entity);
        }
    }

    public class GetCompanySettingsByCurrencyQueryHandler : BaseHandler<GetCompanySettingsByCurrencyQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public GetCompanySettingsByCurrencyQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<GetCompanySettingsByCurrencyQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompanySettingsByCurrencyQuery request, CancellationToken cancellationToken)
        {
            var entities = await _settingsQueryService.GetCompanySettingsByCurrencyAsync(request.CurrencyId, cancellationToken);
            var dtos = entities.Select(s => _mapper.Map<CompanySettingsDto>(s)).ToList();
            return dtos;
        }
    }

    public class GetCompanySettingsByTimeZoneQueryHandler : BaseHandler<GetCompanySettingsByTimeZoneQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public GetCompanySettingsByTimeZoneQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<GetCompanySettingsByTimeZoneQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompanySettingsByTimeZoneQuery request, CancellationToken cancellationToken)
        {
            var entities = await _settingsQueryService.GetCompanySettingsByTimeZoneAsync(request.TimeZone, cancellationToken);
            var dtos = entities.Select(s => _mapper.Map<CompanySettingsDto>(s)).ToList();
            return dtos;
        }
    }

    public class GetCompanySettingsPagedQueryHandler : BaseHandler<GetCompanySettingsPagedQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public GetCompanySettingsPagedQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<GetCompanySettingsPagedQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompanySettingsPagedQuery request, CancellationToken cancellationToken)
        {
            var (entities, total) = await _settingsQueryService.GetCompanySettingsPagedAsync(request.PageIndex, request.PageSize, cancellationToken);
            var dtos = entities.Select(s => _mapper.Map<CompanySettingsDto>(s)).ToList();
            return new CompanySettingsPagedResultDto
            {
                Settings = dtos,
                TotalCount = total
            };
        }
    }

    public class GetCompanySettingsPagedByCurrencyQueryHandler : BaseHandler<GetCompanySettingsPagedByCurrencyQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public GetCompanySettingsPagedByCurrencyQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<GetCompanySettingsPagedByCurrencyQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompanySettingsPagedByCurrencyQuery request, CancellationToken cancellationToken)
        {
            var (entities, total) = await _settingsQueryService.GetCompanySettingsPagedByCurrencyAsync(request.CurrencyId, request.PageIndex, request.PageSize, cancellationToken);
            var dtos = entities.Select(s => _mapper.Map<CompanySettingsDto>(s)).ToList();
            return new CompanySettingsPagedResultDto
            {
                Settings = dtos,
                TotalCount = total
            };
        }
    }

    public class GetCompanySettingsPagedByTimeZoneQueryHandler : BaseHandler<GetCompanySettingsPagedByTimeZoneQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public GetCompanySettingsPagedByTimeZoneQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<GetCompanySettingsPagedByTimeZoneQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompanySettingsPagedByTimeZoneQuery request, CancellationToken cancellationToken)
        {
            var (entities, total) = await _settingsQueryService.GetCompanySettingsPagedByTimeZoneAsync(request.TimeZone, request.PageIndex, request.PageSize, cancellationToken);
            var dtos = entities.Select(s => _mapper.Map<CompanySettingsDto>(s)).ToList();
            return new CompanySettingsPagedResultDto
            {
                Settings = dtos,
                TotalCount = total
            };
        }
    }

    public class SearchCompanySettingsByKeywordQueryHandler : BaseHandler<SearchCompanySettingsByKeywordQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public SearchCompanySettingsByKeywordQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<SearchCompanySettingsByKeywordQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(SearchCompanySettingsByKeywordQuery request, CancellationToken cancellationToken)
        {
            var entities = await _settingsQueryService.SearchCompanySettingsByKeywordAsync(request.Keyword, cancellationToken);
            var dtos = entities.Select(s => _mapper.Map<CompanySettingsDto>(s)).ToList();
            return dtos;
        }
    }

    public class SettingsExistQueryHandler : BaseHandler<SettingsExistQuery>
    {
        private readonly ICompanySettingsValidationService _settingsValidationService;

        public SettingsExistQueryHandler(
            ICompanySettingsValidationService settingsValidationService,
            ILogger<SettingsExistQueryHandler> logger
        ) : base(logger)
        {
            _settingsValidationService = settingsValidationService;
        }

        protected override async Task<object?> HandleRequestAsync(SettingsExistQuery request, CancellationToken cancellationToken)
        {
            var exists = await _settingsValidationService.SettingsExistAsync(request.CompanyId, cancellationToken);
            return new { Exists = exists };
        }
    }

    public class IsCompanySettingsUniqueQueryHandler : BaseHandler<IsCompanySettingsUniqueQuery>
    {
        private readonly ICompanySettingsValidationService _settingsValidationService;

        public IsCompanySettingsUniqueQueryHandler(
            ICompanySettingsValidationService settingsValidationService,
            ILogger<IsCompanySettingsUniqueQueryHandler> logger
        ) : base(logger)
        {
            _settingsValidationService = settingsValidationService;
        }

        protected override async Task<object?> HandleRequestAsync(IsCompanySettingsUniqueQuery request, CancellationToken cancellationToken)
        {
            var isUnique = await _settingsValidationService.IsCompanySettingsUniqueAsync(request.CompanyId, cancellationToken);
            return new { IsUnique = isUnique };
        }
    }

    public class GetCompanySettingsCountQueryHandler : BaseHandler<GetCompanySettingsCountQuery>
    {
        private readonly ICompanySettingsQueryService _settingsQueryService;

        public GetCompanySettingsCountQueryHandler(
            ICompanySettingsQueryService settingsQueryService,
            ILogger<GetCompanySettingsCountQueryHandler> logger
        ) : base(logger)
        {
            _settingsQueryService = settingsQueryService;
        }

        protected override async Task<object?> HandleRequestAsync(GetCompanySettingsCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _settingsQueryService.GetCompanySettingsCountAsync(cancellationToken);
            return new CompanySettingsCountDto { Count = count };
        }
    }

  

    #endregion
}
