using MediatR;
using ErpSwiftCore.Utility;
using ErpSwiftCore.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using ErpSwiftCore.Application.Features.Companies.Companies.Queries;
using ErpSwiftCore.Application.Features.Companies.Companies.Commands;
namespace ErpSwiftCore.API.Controllers.AdminControllers.CompaniesController
{
    [Route("api/company")]
    [ApiController]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region ──────────── Command Endpoints ────────────
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> CreateCompany([FromBody] CompanyCreateDto dto)
        {
            var command = new CreateCompanyCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost("bulk-create")]
        public async Task<ActionResult<APIResponseDto>> BulkCreateCompanies([FromBody] IEnumerable<CompanyCreateDto> dtoList)
        {
            var command = new BulkCreateCompaniesCommand(dtoList);
            APIResponseDto? response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> UpdateCompany([FromBody] CompanyUpdateDto dto)
        {
            var command = new UpdateCompanyCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }






        [HttpDelete("delete/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeleteCompany(Guid companyId)
        {
            var command = new DeleteCompanyCommand(companyId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAllCompanies()
        {
            var command = new DeleteAllCompaniesCommand();
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRangeCompanies(IEnumerable<Guid> Ids)
        {
            var command = new DeleteCompanyRangeCommand(Ids);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }






        [HttpDelete("soft-delete/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> SoftDeleteCompany(Guid companyId)
        {
            var command = new SoftDeleteCompanyCommand(companyId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("soft-delete-all")]
        public async Task<ActionResult<APIResponseDto>> SoftDeleteAllCompanies()
        {
            var command = new SoftDeleteAllCompaniesCommand();
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("soft-delete-range")]
        public async Task<ActionResult<APIResponseDto>> SoftDeleteRangeCompanies(IEnumerable<Guid> Ids)
        {
            var command = new SoftDeleteCompanyRangeCommand(Ids);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }






        [HttpPost("restore/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> RestoreCompany(Guid companyId)
        {
            var command = new RestoreCompanyCommand(companyId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost("restore-all")]
        public async Task<ActionResult<APIResponseDto>> RestoreAllCompanies()
        {
            var command = new RestoreAllCompaniesCommand();
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPost("restore-range")]
        public async Task<ActionResult<APIResponseDto>> RestoreRangeCompanies(IEnumerable<Guid> Ids)
        {
            var command = new RestoreCompanyRangeCommand(Ids);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }







         
        #endregion
        #region ──────────── Query Endpoints ────────────

        /// <summary>
        /// جلب جميع الشركات
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAllCompanies()
        {
            var query = new GetAllCompaniesQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

       

        /// <summary>
        /// جلب شركة بواسطة المعرف
        /// </summary>
        [HttpGet("{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCompanyById(Guid companyId)
        {
            var query = new GetCompanyByIdQuery(companyId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب شركة بواسطة الكود
        /// </summary>
        [HttpGet("code/{companyCode}")]
        public async Task<ActionResult<APIResponseDto>> GetCompanyByCode(string companyCode)
        {
            var query = new GetCompanyByCodeQuery(companyCode);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب شركة بواسطة الاسم
        /// </summary>
        [HttpGet("name/{companyName}")]
        public async Task<ActionResult<APIResponseDto>> GetCompanyByName(string companyName)
        {
            var query = new GetCompanyByNameQuery(companyName);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الشركات حسب البلد
        /// </summary>
        [HttpGet("country/{country}")]
        public async Task<ActionResult<APIResponseDto>> GetCompaniesByCountry(string country)
        {
            var query = new GetCompaniesByCountryQuery(country);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الشركات حسب الصناعة
        /// </summary>
        [HttpGet("industry/{industry}")]
        public async Task<ActionResult<APIResponseDto>> GetCompaniesByIndustry(string industry)
        {
            var query = new GetCompaniesByIndustryQuery(industry);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الشركات حسب مُعرف المالك
        /// </summary>
        [HttpGet("owner/{ownerId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCompaniesByOwnerId(Guid ownerId)
        {
            var query = new GetCompaniesByOwnerIdQuery(ownerId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الشركات مع ترقيم الصفحات (بدون فلتر)
        /// </summary>
        [HttpGet("paged")]
        public async Task<ActionResult<APIResponseDto>> GetCompaniesPaged([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetCompaniesPagedQuery(pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

         

        /// <summary>
        /// جلب الشركات حسب الدولة مع ترقيم الصفحات
        /// </summary>
        [HttpGet("paged/country/{country}")]
        public async Task<ActionResult<APIResponseDto>> GetCompaniesPagedByCountry(string country, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetCompaniesPagedByCountryQuery(country, pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب الشركات حسب الصناعة مع ترقيم الصفحات
        /// </summary>
        [HttpGet("paged/industry/{industry}")]
        public async Task<ActionResult<APIResponseDto>> GetCompaniesPagedByIndustry(string industry, [FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var query = new GetCompaniesPagedByIndustryQuery(industry, pageIndex, pageSize);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// جلب عدد الشركات
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCompaniesCount()
        {
            var query = new GetCompaniesCountQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
        /// <summary>
        /// البحث عن شركات بالكلمة المفتاحية
        /// </summary>
        [HttpGet("search/keyword/{keyword}")]
        public async Task<ActionResult<APIResponseDto>> SearchCompaniesByKeyword(string keyword)
        {
            var query = new SearchCompaniesByKeywordQuery(keyword);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// البحث عن شركات بالاسم
        /// </summary>
        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<APIResponseDto>> SearchCompaniesByName(string name)
        {
            var query = new SearchCompaniesByNameQuery(name);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// البحث عن شركات بالرقم الضريبي
        /// </summary>
        [HttpGet("search/taxid/{taxId}")]
        public async Task<ActionResult<APIResponseDto>> SearchCompaniesByTaxId(string taxId)
        {
            var query = new SearchCompaniesByTaxIdQuery(taxId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        #endregion
    }
}

