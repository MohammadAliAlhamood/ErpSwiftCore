using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Commands;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Dtos;
using ErpSwiftCore.Application.Features.Billings.InvoiceReporting.Queries;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceReportingService;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.BillingsController
{
    [Route("api/invoice-reporting")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class InvoiceReportingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceReportingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Import Commands

        // 1. Import invoices from Excel
        // POST api/invoice-reporting/import/excel
        [HttpPost("import/excel")]
        public async Task<ActionResult<APIResponseDto>> ImportExcel([FromForm] UploadInvoicesFileDto dto)
        {
            var cmd = new ImportInvoicesFromExcelCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 2. Import invoices from CSV
        // POST api/invoice-reporting/import/csv
        [HttpPost("import/csv")]
        public async Task<ActionResult<APIResponseDto>> ImportCsv([FromForm] UploadInvoicesFileDto dto)
        {
            var cmd = new ImportInvoicesFromCsvCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 3. Import invoices from JSON
        // POST api/invoice-reporting/import/json
        [HttpPost("import/json")]
        public async Task<ActionResult<APIResponseDto>> ImportJson([FromForm] UploadInvoicesFileDto dto)
        {
            var cmd = new ImportInvoicesFromJsonCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Export Queries

        // 4. Export all invoices to Excel
        // GET api/invoice-reporting/export/excel
        [HttpGet("export/excel")]
        public async Task<IActionResult> ExportExcel()
        {
            var q = new ExportInvoicesToExcelQuery();
            var stream = await _mediator.Send(q);
            return File((Stream)stream,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "invoices.xlsx");
        }

        // 5. Export all invoices to CSV
        // GET api/invoice-reporting/export/csv
        [HttpGet("export/csv")]
        public async Task<IActionResult> ExportCsv()
        {
            var q = new ExportInvoicesToCsvQuery();
            var stream = await _mediator.Send(q);
            return File((Stream)stream,
                        "text/csv",
                        "invoices.csv");
        }

        // 6. Export all invoices to JSON
        // GET api/invoice-reporting/export/json
        [HttpGet("export/json")]
        public async Task<IActionResult> ExportJson()
        {
            var q = new ExportInvoicesToJsonQuery();
            var stream = await _mediator.Send(q);
            return File((Stream)stream,
                        "application/json",
                        "invoices.json");
        }

        #endregion

        #region Report Queries

        // 7. Get invoice report with optional filters
        // GET api/invoice-reporting/report
        [HttpGet("report")]
        public async Task<ActionResult<APIResponseDto>> GetReport(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] InvoiceStatus? status)
        {
            var filter = new InvoiceReportFilterDto { FromDate = fromDate, ToDate = toDate, Status = status };
            var q = new GetInvoicesReportQuery(filter);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 8. Get invoice total summary grouped by date
        // GET api/invoice-reporting/summary
        [HttpGet("summary")]
        public async Task<ActionResult<APIResponseDto>> GetSummary(
            [FromQuery] DateGrouping grouping,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var q = new GetInvoiceTotalSummaryQuery(grouping, fromDate, toDate);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 9. Get invoice aging report
        // GET api/invoice-reporting/aging/{asOfDate}
        [HttpGet("aging/{asOfDate:datetime}")]
        public async Task<ActionResult<APIResponseDto>> GetAging(DateTime asOfDate)
        {
            var q = new GetInvoiceAgingReportQuery(asOfDate);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 10. Get tax & discount summary
        // GET api/invoice-reporting/taxdiscount
        [HttpGet("taxdiscount")]
        public async Task<ActionResult<APIResponseDto>> GetTaxDiscountSummary(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var q = new GetTaxDiscountSummaryQuery(fromDate, toDate);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Export Selected Queries

        // 11. Export selected invoices to PDF
        // POST api/invoice-reporting/export/pdf
        [HttpPost("export/pdf")]
        public async Task<IActionResult> ExportToPdf([FromBody] IEnumerable<Guid> invoiceIds)
        {
            var q = new ExportInvoicesToPdfQuery(invoiceIds);
            var stream = await _mediator.Send(q);
            return File((Stream)stream, "application/pdf", "invoices.pdf");
        }

        // 12. Export selected invoices to Excel
        // POST api/invoice-reporting/export/excel/selected
        [HttpPost("export/excel/selected")]
        public async Task<IActionResult> ExportSelectedExcel([FromBody] IEnumerable<Guid> invoiceIds)
        {
            var q = new ExportSelectedInvoicesToExcelQuery(invoiceIds);
            var stream = await _mediator.Send(q);
            return File(( Stream)stream,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "selected-invoices.xlsx");
        }

        // 13. Export selected invoices to CSV
        // POST api/invoice-reporting/export/csv/selected
        [HttpPost("export/csv/selected")]
        public async Task<IActionResult> ExportSelectedCsv([FromBody] IEnumerable<Guid> invoiceIds)
        {
            var q = new ExportSelectedInvoicesToCsvQuery(invoiceIds);
            var stream = await _mediator.Send(q);
            return File(( Stream)stream, "text/csv", "selected-invoices.csv");
        }

        #endregion
    }
}
