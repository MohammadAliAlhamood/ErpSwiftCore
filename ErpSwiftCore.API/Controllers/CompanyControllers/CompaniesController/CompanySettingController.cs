using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using System.Threading.Tasks;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Commands;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Dtos;
using ErpSwiftCore.Application.Features.Companies.CompaniesSettings.Queries;
using ErpSwiftCore.Application;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.CompaniesController
{
    [Route("api/company-setting")]
    [ApiController]
    public class CompanySettingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanySettingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region ──────────── Command Endpoints ────────────

        /// <summary>
        /// إنشاء إعدادات شركة جديدة
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> CreateCompanySettings([FromBody] CompanySettingsCreateDto dto)
        {
            var command = new CreateCompanySettingsCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// إنشاء مجموعة إعدادات شركات دفعة واحدة
        /// </summary>
        [HttpPost("bulk-create")]
        public async Task<ActionResult<APIResponseDto>> BulkCreateCompanySettings([FromBody] CompanySettingsBulkCreateDto dto)
        {
            var command = new BulkCreateCompanySettingsCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// تعديل إعدادات شركة بالكامل
        /// </summary>
        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> UpdateCompanySettings([FromBody] CompanySettingsUpdateDto dto)
        {
            var command = new UpdateCompanySettingsCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// تحديث العملة لإعدادات شركة
        /// </summary>
        [HttpPut("update-currency")]
        public async Task<ActionResult<APIResponseDto>> UpdateCompanySettingsCurrency([FromBody] CompanySettingsCurrencyUpdateDto dto)
        {
            var command = new UpdateCompanySettingsCurrencyCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// تحديث المنطقة الزمنية لإعدادات شركة
        /// </summary>
        [HttpPut("update-timezone")]
        public async Task<ActionResult<APIResponseDto>> UpdateCompanySettingsTimeZone([FromBody] CompanySettingsTimeZoneUpdateDto dto)
        {
            var command = new UpdateCompanySettingsTimeZoneCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// حذف إعدادات شركة واحدة
        /// </summary>
        [HttpDelete("delete/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeleteCompanySettings(Guid companyId)
        {
            var command = new DeleteCompanySettingsCommand(companyId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// حذف إعدادات شركات متعددة
        /// </summary>
        [HttpPost("bulk-delete")]
        public async Task<ActionResult<APIResponseDto>> BulkDeleteCompanySettings([FromBody] CompanySettingsBulkDeleteDto dto)
        {
            var command = new BulkDeleteCompanySettingsCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// حذف جميع إعدادات الشركات
        /// </summary>
        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAllCompanySettings()
        {
            var command = new DeleteAllCompanySettingsCommand();
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// استرجاع إعدادات شركة واحدة
        /// </summary>
        [HttpPost("restore/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> RestoreCompanySettings(Guid companyId)
        {
            var command = new RestoreCompanySettingsCommand(companyId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// استرجاع إعدادات شركات متعددة
        /// </summary>
        [HttpPost("bulk-restore")]
        public async Task<ActionResult<APIResponseDto>> BulkRestoreCompanySettings([FromBody] CompanySettingsBulkRestoreDto dto)
        {
            var command = new BulkRestoreCompanySettingsCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// استرجاع جميع إعدادات الشركات
        /// </summary>
        [HttpPost("restore-all")]
        public async Task<ActionResult<APIResponseDto>> RestoreAllCompanySettings()
        {
            var command = new RestoreAllCompanySettingsCommand();
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

         
        #endregion


        #region ──────────── Query Endpoints ────────────

        /// <summary>
        /// جلب جميع إعدادات الشركات
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAllCompanySettings()
        {
            var query = new GetAllCompanySettingsQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب إعدادات الشركات النشطة فقط
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<APIResponseDto>> GetActiveCompanySettings()
        {
            var query = new GetActiveCompanySettingsQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب إعدادات الشركات المؤرشفة فقط
        /// </summary>
        [HttpGet("SoftDeleted")]
        public async Task<ActionResult<APIResponseDto>> GetSoftDeletedCompanySettings()
        {
            var query = new GetSoftDeletedCompanySettingsQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب إعدادات شركة بواسطة المعرّف
        /// </summary>
        [HttpGet("{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCompanySettingsByCompanyId(Guid companyId)
        {
            var query = new GetCompanySettingsByCompanyIdQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب إعدادات الشركات حسب العملة
        /// </summary>
        [HttpGet("by-currency/{currencyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCompanySettingsByCurrency(Guid currencyId)
        {
            var query = new GetCompanySettingsByCurrencyQuery(currencyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب إعدادات الشركات حسب المنطقة الزمنية
        /// </summary>
        [HttpGet("by-timezone/{timeZone}")]
        public async Task<ActionResult<APIResponseDto>> GetCompanySettingsByTimeZone(string timeZone)
        {
            var query = new GetCompanySettingsByTimeZoneQuery(timeZone);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب إعدادات الشركات بترقيم الصفحات
        /// </summary>
        [HttpGet("paged")]
        public async Task<ActionResult<APIResponseDto>> GetCompanySettingsPaged([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetCompanySettingsPagedQuery(pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب إعدادات الشركات بترقيم الصفحات حسب العملة
        /// </summary>
        [HttpGet("paged/by-currency/{currencyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCompanySettingsPagedByCurrency(Guid currencyId, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetCompanySettingsPagedByCurrencyQuery(currencyId, pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب إعدادات الشركات بترقيم الصفحات حسب المنطقة الزمنية
        /// </summary>
        [HttpGet("paged/by-timezone/{timeZone}")]
        public async Task<ActionResult<APIResponseDto>> GetCompanySettingsPagedByTimeZone(string timeZone, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetCompanySettingsPagedByTimeZoneQuery(timeZone, pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// البحث عن إعدادات الشركات بالكلمة المفتاحية
        /// </summary>
        [HttpGet("search/keyword/{keyword}")]
        public async Task<ActionResult<APIResponseDto>> SearchCompanySettingsByKeyword(string keyword)
        {
            var query = new SearchCompanySettingsByKeywordQuery(keyword);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// التحقق من وجود إعدادات شركة بواسطة المعرّف
        /// </summary>
        [HttpGet("exists/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> CompanySettingsExist(Guid companyId)
        {
            var query = new SettingsExistQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// التحقق من تفرد إعدادات شركة بواسطة المعرّف
        /// </summary>
        [HttpGet("exists/unique/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> IsCompanySettingsUnique(Guid companyId)
        {
            var query = new IsCompanySettingsUniqueQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب عدد جميع إعدادات الشركات
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCompanySettingsCount()
        {
            var query = new GetCompanySettingsCountQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب عدد إعدادات الشركات النشطة
        /// </summary>
        [HttpGet("count/active")]
        public async Task<ActionResult<APIResponseDto>> GetActiveCompanySettingsCount()
        {
            var query = new GetActiveCompanySettingsCountQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        #endregion
    }
}
