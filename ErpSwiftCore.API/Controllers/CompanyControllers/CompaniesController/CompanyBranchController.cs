using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using System.Threading.Tasks;
using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Commands;
using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Dtos;
using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Queries;
using ErpSwiftCore.Application;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.CompaniesController
{
    [Route("api/company-branch")]
    [ApiController]
    public class CompanyBranchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyBranchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region ──────────── Command Endpoints ────────────

        /// <summary>
        /// إنشاء فرع جديد
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> CreateBranch([FromBody] CompanyBranchCreateDto dto)
        {
            var command = new CreateCompanyBranchCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

 

        /// <summary>
        /// تعديل بيانات فرع
        /// </summary>
        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> UpdateBranch([FromBody] CompanyBranchUpdateDto dto)
        {
            var command = new UpdateCompanyBranchCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// حذف فرع واحد
        /// </summary>
        [HttpDelete("delete/{branchId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeleteBranch(Guid branchId)
        {
            var command = new DeleteCompanyBranchCommand(branchId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

 
        /// <summary>
        /// حذف جميع الفروع لشركة معينة
        /// </summary>
        [HttpDelete("delete-all/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeleteAllBranches(Guid companyId)
        {
            var command = new DeleteAllCompanyBranchesCommand(companyId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// استرجاع فرع واحد
        /// </summary>
        [HttpPost("restore/{branchId:guid}")]
        public async Task<ActionResult<APIResponseDto>> RestoreBranch(Guid branchId)
        {
            var command = new RestoreCompanyBranchCommand(branchId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

 

        /// <summary>
        /// استرجاع جميع فروع الشركة
        /// </summary>
        [HttpPost("restore-all/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> RestoreAllBranches(Guid companyId)
        {
            var command = new RestoreAllCompanyBranchesCommand(companyId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// تعيين حالة نشط/غير نشط لفرع واحد
        /// </summary>
        [HttpPost("set-active/{branchId:guid}")]
        public async Task<ActionResult<APIResponseDto>> SetBranchActiveStatus(Guid branchId, [FromQuery] bool isActive)
        {
            var command = new SetCompanyBranchActiveStatusCommand(branchId, isActive);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

 

        #endregion


        #region ──────────── Query Endpoints ────────────

        /// <summary>
        /// جلب جميع الفروع
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAllBranches()
        {
            var query = new GetAllBranchesQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب فرع بواسطة المعرف
        /// </summary>
        [HttpGet("{branchId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetBranchById(Guid branchId)
        {
            var query = new GetBranchByIdQuery(branchId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب فرع مع بيانات الشركة الخاصة به
        /// </summary>
        [HttpGet("with-company/{branchId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetBranchWithCompany(Guid branchId)
        {
            var query = new GetBranchWithCompanyQuery(branchId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب جميع الفروع التابعة لشركة معينة
        /// </summary>
        [HttpGet("by-company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetBranchesByCompanyId(Guid companyId)
        {
            var query = new GetBranchesByCompanyIdQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الفروع النشطة لشركة معينة
        /// </summary>
        [HttpGet("active/by-company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetActiveBranchesByCompanyId(Guid companyId)
        {
            var query = new GetActiveBranchesByCompanyIdQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الفروع المؤرشفة لشركة معينة
        /// </summary>
        [HttpGet("SoftDeleted/by-company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSoftDeletedBranchesByCompanyId(Guid companyId)
        {
            var query = new GetSoftDeletedBranchesByCompanyIdQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الفروع بترقيم الصفحات (بدون فلتر)
        /// </summary>
        [HttpGet("paged/by-company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetBranchesPaged(Guid companyId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetBranchesPagedQuery(companyId, pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الفروع النشطة بترقيم الصفحات
        /// </summary>
        [HttpGet("paged/active/by-company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetActiveBranchesPaged(Guid companyId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetActiveBranchesPagedQuery(companyId, pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الفروع المؤرشفة بترقيم الصفحات
        /// </summary>
        [HttpGet("paged/SoftDeleted/by-company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSoftDeletedBranchesPaged(Guid companyId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetSoftDeletedBranchesPagedQuery(companyId, pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// البحث عن الفروع بالاسم
        /// </summary>
        [HttpGet("search/name/{companyId:guid}/{name}")]
        public async Task<ActionResult<APIResponseDto>> SearchBranchesByName(Guid companyId, string name)
        {
            var query = new SearchBranchesByNameQuery(companyId, name);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// البحث عن الفروع بالكود
        /// </summary>
        [HttpGet("search/code/{companyId:guid}/{code}")]
        public async Task<ActionResult<APIResponseDto>> SearchBranchesByCode(Guid companyId, string code)
        {
            var query = new SearchBranchesByCodeQuery(companyId, code);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// البحث عن الفروع بالكلمة المفتاحية
        /// </summary>
        [HttpGet("search/keyword/{companyId:guid}/{keyword}")]
        public async Task<ActionResult<APIResponseDto>> SearchBranchesByKeyword(Guid companyId, string keyword)
        {
            var query = new SearchBranchesByKeywordQuery(companyId, keyword);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// التحقق من وجود فرع بواسطة المعرف
        /// </summary>
        [HttpGet("exists/{branchId:guid}")]
        public async Task<ActionResult<APIResponseDto>> BranchExists(Guid branchId)
        {
            var query = new BranchExistsQuery(branchId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// التحقق من وجود فرع بواسطة الكود ضمن شركة معينة
        /// </summary>
        [HttpGet("exists/code/{companyId:guid}/{branchCode}")]
        public async Task<ActionResult<APIResponseDto>> BranchExistsByCode(Guid companyId, string branchCode)
        {
            var query = new BranchExistsByCodeQuery(companyId, branchCode);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// التحقق من تفرد اسم الفرع ضمن شركة معينة
        /// </summary>
        [HttpGet("exists/name-unique/{companyId:guid}/{branchName}")]
        public async Task<ActionResult<APIResponseDto>> IsBranchNameUnique(Guid companyId, string branchName, [FromQuery] Guid? excludeBranchId)
        {
            var query = new IsBranchNameUniqueQuery(companyId, branchName, excludeBranchId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// التحقق مما إذا كانت الشركة تحتوي على أي فروع
        /// </summary>
        [HttpGet("has/company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> HasBranches(Guid companyId)
        {
            var query = new HasBranchesQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب عدد الفروع لشركة معينة
        /// </summary>
        [HttpGet("count/company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetBranchesCount(Guid companyId)
        {
            var query = new GetBranchesCountQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب عدد الفروع النشطة لشركة معينة
        /// </summary>
        [HttpGet("count/active/company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetActiveBranchesCount(Guid companyId)
        {
            var query = new GetActiveBranchesCountQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        #endregion
    }
}
