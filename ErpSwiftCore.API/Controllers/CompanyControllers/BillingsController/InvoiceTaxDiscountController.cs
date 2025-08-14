using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Commands;
using ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Dtos;
using ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.BillingsController
{
    [Route("api/invoice-tax")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class InvoiceTaxDiscountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceTaxDiscountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        // 1. Get all taxes for an invoice
        // GET api/invoice-tax/{invoiceId}/taxes
        [HttpGet("{invoiceId:guid}/taxes")]
        public async Task<ActionResult<APIResponseDto>> GetTaxesByInvoice(Guid invoiceId)
        {
            var q = new GetTaxesByInvoiceQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 2. Get taxes count
        // GET api/invoice-tax/{invoiceId}/taxes/count
        [HttpGet("{invoiceId:guid}/taxes/count")]
        public async Task<ActionResult<APIResponseDto>> GetTaxesCount(Guid invoiceId)
        {
            var q = new GetTaxesCountQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 3. Get a single tax by ID
        // GET api/invoice-tax/taxes/{taxId}
        [HttpGet("taxes/{taxId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetTaxById(Guid taxId)
        {
            var q = new GetTaxByIdQuery(taxId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 4. Get all discounts for an invoice
        // GET api/invoice-tax/{invoiceId}/discounts
        [HttpGet("{invoiceId:guid}/discounts")]
        public async Task<ActionResult<APIResponseDto>> GetDiscountsByInvoice(Guid invoiceId)
        {
            var q = new GetDiscountsByInvoiceQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 5. Get discounts count
        // GET api/invoice-tax/{invoiceId}/discounts/count
        [HttpGet("{invoiceId:guid}/discounts/count")]
        public async Task<ActionResult<APIResponseDto>> GetDiscountsCount(Guid invoiceId)
        {
            var q = new GetDiscountsCountQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 6. Get a single discount by ID
        // GET api/invoice-tax/discounts/{discountId}
        [HttpGet("discounts/{discountId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetDiscountById(Guid discountId)
        {
            var q = new GetDiscountByIdQuery(discountId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 7. Get total tax amount
        // GET api/invoice-tax/{invoiceId}/taxes/total
        [HttpGet("{invoiceId:guid}/taxes/total")]
        public async Task<ActionResult<APIResponseDto>> GetTotalTaxAmount(Guid invoiceId)
        {
            var q = new GetTotalTaxAmountQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 8. Get total discount amount
        // GET api/invoice-tax/{invoiceId}/discounts/total
        [HttpGet("{invoiceId:guid}/discounts/total")]
        public async Task<ActionResult<APIResponseDto>> GetTotalDiscountAmount(Guid invoiceId)
        {
            var q = new GetTotalDiscountAmountQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 9. Validate tax & discount setup for an invoice
        // GET api/invoice-tax/validate/{invoiceId}
        [HttpGet("validate/{invoiceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> ValidateTaxAndDiscount(Guid invoiceId)
        {
            var q = new ValidateTaxAndDiscountQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 10. Check if a tax exists
        // GET api/invoice-tax/validate/tax/{taxId}
        [HttpGet("validate/tax/{taxId:guid}")]
        public async Task<ActionResult<APIResponseDto>> TaxExists(Guid taxId)
        {
            var q = new TaxExistsQuery(taxId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 11. Check if a discount exists
        // GET api/invoice-tax/validate/discount/{discountId}
        [HttpGet("validate/discount/{discountId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DiscountExists(Guid discountId)
        {
            var q = new DiscountExistsQuery(discountId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 12. Check if invoice has any taxes
        // GET api/invoice-tax/validate/has-taxes/{invoiceId}
        [HttpGet("validate/has-taxes/{invoiceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> HasTaxes(Guid invoiceId)
        {
            var q = new HasTaxesQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 13. Check if invoice has any discounts
        // GET api/invoice-tax/validate/has-discounts/{invoiceId}
        [HttpGet("validate/has-discounts/{invoiceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> HasDiscounts(Guid invoiceId)
        {
            var q = new HasDiscountsQuery(invoiceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 14. Check if invoice is linked to a given currency
        // GET api/invoice-tax/validate/linked-currency/{invoiceId}/{currencyId}
        [HttpGet("validate/linked-currency/{invoiceId:guid}/{currencyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> IsLinkedToCurrency(Guid invoiceId, Guid currencyId)
        {
            var q = new IsInvoiceLinkedToCurrencyQuery(invoiceId, currencyId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region Commands

        // 1. Create taxes and discounts
        // POST api/invoice-tax/create
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] CreateTaxesAndDiscountsDto dto)
        {
            var cmd = new CreateTaxesAndDiscountsCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 2. Update taxes and discounts
        // PUT api/invoice-tax/update
        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] UpdateTaxesAndDiscountsDto dto)
        {
            var cmd = new UpdateTaxesAndDiscountsCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 3. Add single tax
        // POST api/invoice-tax/{invoiceId}/tax/add
        [HttpPost("{invoiceId:guid}/tax/add")]
        public async Task<ActionResult<APIResponseDto>> AddTax(Guid invoiceId, [FromBody] CreateInvoiceTaxDto dto)
        {
            var cmd = new AddInvoiceTaxCommand(invoiceId, dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 4. Add multiple taxes
        // POST api/invoice-tax/{invoiceId}/taxes/add-multiple
        [HttpPost("{invoiceId:guid}/taxes/add-multiple")]
        public async Task<ActionResult<APIResponseDto>> AddTaxes(Guid invoiceId, [FromBody] IEnumerable<CreateInvoiceTaxDto> dtos)
        {
            var cmd = new AddInvoiceTaxesCommand(invoiceId, dtos);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 5. Update tax
        // PUT api/invoice-tax/tax/update
        [HttpPut("tax/update")]
        public async Task<ActionResult<APIResponseDto>> UpdateTax([FromBody] UpdateInvoiceTaxDto dto)
        {
            var cmd = new UpdateInvoiceTaxCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 6. Delete tax
        // DELETE api/invoice-tax/tax/delete/{taxId}
        [HttpDelete("tax/delete/{taxId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeleteTax(Guid taxId)
        { 
            var cmd = new DeleteInvoiceTaxCommand(taxId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 7. Delete all taxes of invoice
        // DELETE api/invoice-tax/{invoiceId}/taxes/delete-all
        [HttpDelete("{invoiceId:guid}/taxes/delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAllTaxes(Guid invoiceId)
        {
            var cmd = new DeleteAllTaxesOfInvoiceCommand(invoiceId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 8. Add single discount
        // POST api/invoice-tax/{invoiceId}/discount/add
        [HttpPost("{invoiceId:guid}/discount/add")]
        public async Task<ActionResult<APIResponseDto>> AddDiscount(Guid invoiceId, [FromBody] CreateInvoiceDiscountDto dto)
        {
            var cmd = new AddInvoiceDiscountCommand(invoiceId, dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 9. Add multiple discounts
        // POST api/invoice-tax/{invoiceId}/discounts/add-multiple
        [HttpPost("{invoiceId:guid}/discounts/add-multiple")]
        public async Task<ActionResult<APIResponseDto>> AddDiscounts(Guid invoiceId, [FromBody] IEnumerable<CreateInvoiceDiscountDto> dtos)
        {
            var cmd = new AddInvoiceDiscountsCommand(invoiceId, dtos);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 10. Update discount
        // PUT api/invoice-tax/discount/update
        [HttpPut("discount/update")]
        public async Task<ActionResult<APIResponseDto>> UpdateDiscount([FromBody] UpdateInvoiceDiscountDto dto)
        {
            var cmd = new UpdateInvoiceDiscountCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 11. Delete discount
        // DELETE api/invoice-tax/discount/delete/{discountId}
        [HttpDelete("discount/delete/{discountId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeleteDiscount(Guid discountId)
        { 
            var cmd = new DeleteInvoiceDiscountCommand(discountId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 12. Delete all discounts of invoice
        // DELETE api/invoice-tax/{invoiceId}/discounts/delete-all
        [HttpDelete("{invoiceId:guid}/discounts/delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAllDiscounts(Guid invoiceId)
        {
            var cmd = new DeleteAllDiscountsOfInvoiceCommand(invoiceId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
    }
}
