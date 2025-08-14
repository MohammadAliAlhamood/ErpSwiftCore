using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanySettingsService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Handlers.CommandsHandler
{
    #region ──────────── CompanySettings Command Handlers ────────────

    public class CreateCompanySettingsCommandHandler : BaseHandler<CreateCompanySettingsCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public CreateCompanySettingsCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<CreateCompanySettingsCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(CreateCompanySettingsCommand request, CancellationToken cancellationToken)
        {
            // تحويل DTO إلى Entity وضبط CompanyID
            var entity = _mapper.Map<Domain.EntityCompany. CompanySettings >(request.Settings);
            entity.CompanyID = request.Settings.CompanyId;

            // إنشاء الإعدادات باستخدام خدمة الأوامر
            var newSettingsId = await _settingsCommandService.CreateCompanySettingsAsync(entity, cancellationToken);

            // جلب الإعدادات حديثة الإنشاء بواسطة خدمة الاستعلامات
            var createdEntity = await _settingsQueryService.GetCompanySettingsByCompanyIdAsync(entity.CompanyID, cancellationToken);
            var dto = _mapper.Map<CompanySettingsDto>(createdEntity!);
            return dto;
        }
    }

    public class BulkCreateCompanySettingsCommandHandler : BaseHandler<BulkCreateCompanySettingsCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;
        private readonly IMapper _mapper;

        public BulkCreateCompanySettingsCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            IMapper mapper,
            ILogger<BulkCreateCompanySettingsCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(BulkCreateCompanySettingsCommand request, CancellationToken cancellationToken)
        {
            // تحويل قائمة DTOs إلى Entities وضبط CompanyID لكل مجموعة إعدادات
            var entities = request._Dto.SettingsList.Select(dto =>
            {
                var e = _mapper.Map<Domain.EntityCompany.CompanySettings>(dto);
                e.CompanyID = dto.CompanyId;
                return e;
            }).ToList();

            // إضافة مجموعة إعدادات الشركات باستخدام خدمة الأوامر
            var newIds = await _settingsCommandService.AddCompanySettingsRangeAsync(entities, cancellationToken);

            return new { CreatedSettingsIds = newIds.ToList() };
        }
    }

    public class UpdateCompanySettingsCommandHandler : BaseHandler<UpdateCompanySettingsCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public UpdateCompanySettingsCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<UpdateCompanySettingsCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateCompanySettingsCommand request, CancellationToken cancellationToken)
        {
            // تحويل DTO إلى Entity
            var entity = _mapper.Map<Domain.EntityCompany.CompanySettings>(request.Settings);

            // تحديث الإعدادات باستخدام خدمة الأوامر
            var success = await _settingsCommandService.UpdateCompanySettingsAsync(entity, cancellationToken);
            if (!success)
                throw new DomainException($"تعذر تحديث إعدادات الشركة '{entity.CompanyID}'.");

            // جلب الكائن المحدث بواسطة خدمة الاستعلامات
            var updatedEntity = await _settingsQueryService.GetCompanySettingsByCompanyIdAsync(entity.CompanyID, cancellationToken);
            var dto = _mapper.Map<CompanySettingsDto>(updatedEntity!);
            return dto;
        }
    }

    public class UpdateCompanySettingsCurrencyCommandHandler : BaseHandler<UpdateCompanySettingsCurrencyCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public UpdateCompanySettingsCurrencyCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<UpdateCompanySettingsCurrencyCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateCompanySettingsCurrencyCommand request, CancellationToken cancellationToken)
        {
            var payload = request.Payload;

            // تحديث العملة باستخدام خدمة الأوامر
            var success = await _settingsCommandService.UpdateCompanyCurrencyAsync(payload.CompanyId, payload.CurrencyId, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"إعدادات الشركة '{payload.CompanyId}' غير موجودة.");

            // جلب الكائن المحدث بواسطة خدمة الاستعلامات
            var updatedEntity = await _settingsQueryService.GetCompanySettingsByCompanyIdAsync(payload.CompanyId, cancellationToken);
            var dto = _mapper.Map<CompanySettingsDto>(updatedEntity!);
            return dto;
        }
    }

    public class UpdateCompanySettingsTimeZoneCommandHandler : BaseHandler<UpdateCompanySettingsTimeZoneCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;
        private readonly ICompanySettingsQueryService _settingsQueryService;
        private readonly IMapper _mapper;

        public UpdateCompanySettingsTimeZoneCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ICompanySettingsQueryService settingsQueryService,
            IMapper mapper,
            ILogger<UpdateCompanySettingsTimeZoneCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
            _settingsQueryService = settingsQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateCompanySettingsTimeZoneCommand request, CancellationToken cancellationToken)
        {
            var payload = request.Payload;

            // تحديث المنطقة الزمنية باستخدام خدمة الأوامر
            var success = await _settingsCommandService.UpdateCompanyTimeZoneAsync(payload.CompanyId, payload.TimeZone, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"إعدادات الشركة '{payload.CompanyId}' غير موجودة.");

            // جلب الكائن المحدث بواسطة خدمة الاستعلامات
            var updatedEntity = await _settingsQueryService.GetCompanySettingsByCompanyIdAsync(payload.CompanyId, cancellationToken);
            var dto = _mapper.Map<CompanySettingsDto>(updatedEntity!);
            return dto;
        }
    }

    public class DeleteCompanySettingsCommandHandler : BaseHandler<DeleteCompanySettingsCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;

        public DeleteCompanySettingsCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ILogger<DeleteCompanySettingsCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteCompanySettingsCommand request, CancellationToken cancellationToken)
        {
            // حذف إعدادات الشركة باستخدام خدمة الأوامر
            var success = await _settingsCommandService.DeleteCompanySettingsAsync(request.CompanyId, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"إعدادات الشركة '{request.CompanyId}' غير موجودة.");
            return new { Message = $"تمّ حذف إعدادات الشركة '{request.CompanyId}' بنجاح." };
        }
    }

    public class BulkDeleteCompanySettingsCommandHandler : BaseHandler<BulkDeleteCompanySettingsCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;

        public BulkDeleteCompanySettingsCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ILogger<BulkDeleteCompanySettingsCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(BulkDeleteCompanySettingsCommand request, CancellationToken cancellationToken)
        {
            var deleted = new List<Guid>();
            foreach (var id in request._Dto.CompanyIds)
            {
                var success = await _settingsCommandService.DeleteCompanySettingsAsync(id, cancellationToken);
                if (success) deleted.Add(id);
            }
            return new { DeletedSettingsCompanyIds = deleted };
        }
    }

    public class DeleteAllCompanySettingsCommandHandler : BaseHandler<DeleteAllCompanySettingsCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;

        public DeleteAllCompanySettingsCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ILogger<DeleteAllCompanySettingsCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteAllCompanySettingsCommand request, CancellationToken cancellationToken)
        {
            // حذف جميع إعدادات الشركات باستخدام خدمة الأوامر
            var success = await _settingsCommandService.DeleteAllCompanySettingsAsync(cancellationToken);
            if (!success)
                throw new DomainException("فشل حذف جميع إعدادات الشركات.");
            return new { Message = "تمّ حذف جميع إعدادات الشركات بنجاح." };
        }
    }

    public class RestoreCompanySettingsCommandHandler : BaseHandler<RestoreCompanySettingsCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;

        public RestoreCompanySettingsCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ILogger<RestoreCompanySettingsCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreCompanySettingsCommand request, CancellationToken cancellationToken)
        {
            // استعادة إعدادات الشركة باستخدام خدمة الأوامر
            var success = await _settingsCommandService.RestoreCompanySettingsAsync(request.CompanyId, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"إعدادات الشركة '{request.CompanyId}' غير موجودة أو غير مؤرشفة.");
            return new { Message = $"تمّ استرجاع إعدادات الشركة '{request.CompanyId}' بنجاح." };
        }
    }

    public class BulkRestoreCompanySettingsCommandHandler : BaseHandler<BulkRestoreCompanySettingsCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;

        public BulkRestoreCompanySettingsCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ILogger<BulkRestoreCompanySettingsCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(BulkRestoreCompanySettingsCommand request, CancellationToken cancellationToken)
        {
            var restored = new List<Guid>();
            foreach (var id in request._Dto.CompanyIds)
            {
                var success = await _settingsCommandService.RestoreCompanySettingsAsync(id, cancellationToken);
                if (success) restored.Add(id);
            }
            return new { RestoredSettingsCompanyIds = restored };
        }
    }

    public class RestoreAllCompanySettingsCommandHandler : BaseHandler<RestoreAllCompanySettingsCommand>
    {
        private readonly ICompanySettingsCommandService _settingsCommandService;

        public RestoreAllCompanySettingsCommandHandler(
            ICompanySettingsCommandService settingsCommandService,
            ILogger<RestoreAllCompanySettingsCommandHandler> logger
        ) : base(logger)
        {
            _settingsCommandService = settingsCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreAllCompanySettingsCommand request, CancellationToken cancellationToken)
        {
            // استعادة جميع إعدادات الشركات باستخدام خدمة الأوامر
            var success = await _settingsCommandService.RestoreAllCompanySettingsAsync(cancellationToken);
            if (!success)
                throw new DomainException("فشل استرجاع جميع إعدادات الشركات.");
            return new { Message = "تمّ استرجاع جميع إعدادات الشركات بنجاح." };
        }
    }

   

   

    #endregion
}
