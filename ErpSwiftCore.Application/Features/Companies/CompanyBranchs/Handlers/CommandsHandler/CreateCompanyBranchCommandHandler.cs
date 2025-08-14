using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Commands;
using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Dtos;
using ErpSwiftCore.Domain.IServices.ICompanyServices.ICompanyBranchService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Handlers.CommandsHandler
{
    #region ──────────── CompanyBranch Command Handlers ────────────

    public class CreateCompanyBranchCommandHandler : BaseHandler<CreateCompanyBranchCommand>
    {
        private readonly ICompanyBranchCommandService _branchCommandService;
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public CreateCompanyBranchCommandHandler(
            ICompanyBranchCommandService branchCommandService,
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<CreateCompanyBranchCommandHandler> logger
        ) : base(logger)
        {
            _branchCommandService = branchCommandService;
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(CreateCompanyBranchCommand request, CancellationToken cancellationToken)
        {
            // تحويل DTO إلى Entity وضبط CompanyID
            var entity = _mapper.Map<SharedKernel.Entities.CompanyBranch>(request.Branch);
            // إنشاء الفرع باستخدام خدمة الأوامر
            var newBranchId = await _branchCommandService.CreateBranchAsync(entity, cancellationToken);

            // جلب الفرع حديث الإنشاء باستخدام خدمة الاستعلامات
            var createdEntity = await _branchQueryService.GetBranchByIdAsync(newBranchId, cancellationToken);
            var dto = _mapper.Map<CompanyBranchDto>(createdEntity!);
            return dto;
        }
    }

    public class BulkCreateCompanyBranchesCommandHandler : BaseHandler<BulkCreateCompanyBranchesCommand>
    {
        private readonly ICompanyBranchCommandService _branchCommandService;
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public BulkCreateCompanyBranchesCommandHandler(
            ICompanyBranchCommandService branchCommandService,
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<BulkCreateCompanyBranchesCommandHandler> logger
        ) : base(logger)
        {
            _branchCommandService = branchCommandService;
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(BulkCreateCompanyBranchesCommand request, CancellationToken cancellationToken)
        {
            // تحويل قائمة DTOs إلى Entities وضبط CompanyID لكل فرع
            var entities = request.Branches.Select(dto =>
            {
                var e = _mapper.Map<SharedKernel.Entities.CompanyBranch>(dto);
                e.CompanyID = request.CompanyId;
                return e;
            }).ToList();

            // إضافة مجموعة الفروع باستخدام خدمة الأوامر
            var newIds = await _branchCommandService.AddBranchesRangeAsync(entities, cancellationToken);

            return new { CreatedBranchIds = newIds.ToList() };
        }
    }

    public class UpdateCompanyBranchCommandHandler : BaseHandler<UpdateCompanyBranchCommand>
    {
        private readonly ICompanyBranchCommandService _branchCommandService;
        private readonly ICompanyBranchQueryService _branchQueryService;
        private readonly IMapper _mapper;

        public UpdateCompanyBranchCommandHandler(
            ICompanyBranchCommandService branchCommandService,
            ICompanyBranchQueryService branchQueryService,
            IMapper mapper,
            ILogger<UpdateCompanyBranchCommandHandler> logger
        ) : base(logger)
        {
            _branchCommandService = branchCommandService;
            _branchQueryService = branchQueryService;
            _mapper = mapper;
        }

        protected override async Task<object?> HandleRequestAsync(UpdateCompanyBranchCommand request, CancellationToken cancellationToken)
        {
            // تحويل DTO إلى Entity مع ضبط المعرف و CompanyID
            var entity = _mapper.Map<SharedKernel.Entities. CompanyBranch>(request.Branch);
            entity.ID = request.Branch.Id; 
            // تحديث الفرع باستخدام خدمة الأوامر
            var success = await _branchCommandService.UpdateBranchAsync(entity, cancellationToken);
            if (!success)
                throw new DomainException($"تعذر تحديث الفرع بالمعرّف '{entity.ID}'.");

            // جلب الكائن المحدث باستخدام خدمة الاستعلامات
            var updatedEntity = await _branchQueryService.GetBranchByIdAsync(entity.ID, cancellationToken);
            var dto = _mapper.Map<CompanyBranchDto>(updatedEntity!);
            return dto;
        }
    }

    public class DeleteCompanyBranchCommandHandler : BaseHandler<DeleteCompanyBranchCommand>
    {
        private readonly ICompanyBranchCommandService _branchCommandService;

        public DeleteCompanyBranchCommandHandler(
            ICompanyBranchCommandService branchCommandService,
            ILogger<DeleteCompanyBranchCommandHandler> logger
        ) : base(logger)
        {
            _branchCommandService = branchCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteCompanyBranchCommand request, CancellationToken cancellationToken)
        {
            // حذف الفرع باستخدام خدمة الأوامر
            var success = await _branchCommandService.DeleteBranchAsync(request.BranchId, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"الفرع بالمعرّف '{request.BranchId}' ");
            return new { Message = $"تمّ حذف الفرع '{request.BranchId}' بنجاح." };
        }
    }

    public class BulkDeleteCompanyBranchesCommandHandler : BaseHandler<BulkDeleteCompanyBranchesCommand>
    {
        private readonly ICompanyBranchCommandService _branchCommandService;

        public BulkDeleteCompanyBranchesCommandHandler(
            ICompanyBranchCommandService branchCommandService,
            ILogger<BulkDeleteCompanyBranchesCommandHandler> logger
        ) : base(logger)
        {
            _branchCommandService = branchCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(BulkDeleteCompanyBranchesCommand request, CancellationToken cancellationToken)
        {
            var deleted = new List<Guid>();
            foreach (var id in request.BranchIds)
            {
                var success = await _branchCommandService.DeleteBranchAsync(id, cancellationToken);
                if (success) deleted.Add(id);
            }
            return new { DeletedBranchIds = deleted };
        }
    }

    public class DeleteAllCompanyBranchesCommandHandler : BaseHandler<DeleteAllCompanyBranchesCommand>
    {
        private readonly ICompanyBranchCommandService _branchCommandService;

        public DeleteAllCompanyBranchesCommandHandler(
            ICompanyBranchCommandService branchCommandService,
            ILogger<DeleteAllCompanyBranchesCommandHandler> logger
        ) : base(logger)
        {
            _branchCommandService = branchCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(DeleteAllCompanyBranchesCommand request, CancellationToken cancellationToken)
        {
            // حذف جميع فروع الشركة باستخدام خدمة الأوامر
            var success = await _branchCommandService.DeleteAllBranchesOfCompanyAsync(request.CompanyId, cancellationToken);
            if (!success)
                throw new DomainException($"فشل حذف جميع الفروع للشركة '{request.CompanyId}'.");
            return new { Message = $"تمّ حذف جميع فروع الشركة '{request.CompanyId}' بنجاح." };
        }
    }

    public class RestoreCompanyBranchCommandHandler : BaseHandler<RestoreCompanyBranchCommand>
    {
        private readonly ICompanyBranchCommandService _branchCommandService;

        public RestoreCompanyBranchCommandHandler(
            ICompanyBranchCommandService branchCommandService,
            ILogger<RestoreCompanyBranchCommandHandler> logger
        ) : base(logger)
        {
            _branchCommandService = branchCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreCompanyBranchCommand request, CancellationToken cancellationToken)
        {
            var success = await _branchCommandService.RestoreBranchAsync(request.BranchId, cancellationToken);
            if (!success)
                throw new DomainNotFoundException($"الفرع بالمعرّف '{request.BranchId}' غير موجود أو غير مؤرشف.");
            return new { Message = $"تمّ استرجاع الفرع '{request.BranchId}' بنجاح." };
        }
    }

    public class BulkRestoreCompanyBranchesCommandHandler : BaseHandler<BulkRestoreCompanyBranchesCommand>
    {
        private readonly ICompanyBranchCommandService _branchCommandService;

        public BulkRestoreCompanyBranchesCommandHandler(
            ICompanyBranchCommandService branchCommandService,
            ILogger<BulkRestoreCompanyBranchesCommandHandler> logger
        ) : base(logger)
        {
            _branchCommandService = branchCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(BulkRestoreCompanyBranchesCommand request, CancellationToken cancellationToken)
        {
            var restored = new List<Guid>();
            foreach (var id in request.BranchIds)
            {
                var success = await _branchCommandService.RestoreBranchAsync(id, cancellationToken);
                if (success) restored.Add(id);
            }
            return new { RestoredBranchIds = restored };
        }
    }

    public class RestoreAllCompanyBranchesCommandHandler : BaseHandler<RestoreAllCompanyBranchesCommand>
    {
        private readonly ICompanyBranchCommandService _branchCommandService;

        public RestoreAllCompanyBranchesCommandHandler(
            ICompanyBranchCommandService branchCommandService,
            ILogger<RestoreAllCompanyBranchesCommandHandler> logger
        ) : base(logger)
        {
            _branchCommandService = branchCommandService;
        }

        protected override async Task<object?> HandleRequestAsync(RestoreAllCompanyBranchesCommand request, CancellationToken cancellationToken)
        {
            var success = await _branchCommandService.RestoreAllBranchesOfCompanyAsync(request.CompanyId, cancellationToken);
            if (!success)
                throw new DomainException($"فشل استرجاع جميع الفروع للشركة '{request.CompanyId}'.");
            return new { Message = $"تمّ استرجاع جميع فروع الشركة '{request.CompanyId}' بنجاح." };
        }
    }

  
 

    #endregion
}
