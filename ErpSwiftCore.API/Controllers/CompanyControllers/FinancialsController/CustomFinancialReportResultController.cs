using ErpSwiftCore.Application; 
using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Commands;
using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Dtos;
using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.FinancialsController
{
    [Route("api/custom-financial-report-result")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class CustomFinancialReportResultController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomFinancialReportResultController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var q = new GetReportByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll([FromQuery] bool includeDeleted = false)
        {
            var q = new GetAllReportsQuery(includeDeleted);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-company/{companyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByCompany(Guid companyId)
        {
            var q = new GetReportsByCompanyQuery(companyId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("export/excel/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> ExportToExcel(Guid id)
        {
            var q = new ExportReportToExcelQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("export/csv/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> ExportToCsv(Guid id)
        {
            var q = new ExportReportToCsvQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("recent/{topCount:int}")]
        public async Task<ActionResult<APIResponseDto>> GetRecent(int topCount)
        {
            var q = new GetRecentReportsQuery(topCount);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count/by-company")]
        public async Task<ActionResult<APIResponseDto>> GetCountByCompany()
        {
            var q = new GetReportsCountByCompanyQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetReportsCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
 

        #endregion

        #region Commands

        [HttpPost("save")]
        public async Task<ActionResult<APIResponseDto>> Save([FromBody] SaveReportDto dto)
        {
            var cmd = new SaveReportCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("save-range")]
        public async Task<ActionResult<APIResponseDto>> SaveRange([FromBody] SaveReportsRangeDto dto)
        {
            var cmd = new SaveReportsRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid id)
        {
            var cmd = new DeleteReportCommand(  id );
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ReportIds)
        {
            var cmd = new DeleteReportsRangeCommand(ReportIds);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("restore/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Restore(Guid id)
        {
            var cmd = new RestoreReportCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("validate")]
        public async Task<ActionResult<APIResponseDto>> Validate([FromBody] ValidateReportDto dto)
        {
            var cmd = new ValidateReportCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
