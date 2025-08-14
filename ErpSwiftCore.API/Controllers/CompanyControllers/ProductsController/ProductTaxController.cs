using MediatR;
using ErpSwiftCore.Utility;
using Microsoft.AspNetCore.Mvc;
using ErpSwiftCore.Application;
using Microsoft.AspNetCore.Authorization;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands;
namespace ErpSwiftCore.API.Controllers.CompanyControllers.ProductsController
{
    [Route("api/product-tax")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class ProductTaxController : ControllerBase
    { 
        private readonly IMediator _mediator;
        public ProductTaxController(IMediator mediator) => _mediator = mediator;

        #region ──────────── Command Endpoints ────────────
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] ProductTaxCreateDto dto)
        {
            var cmd = new CreateProductTaxCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }


        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] ProductTaxUpdateDto dto)
        {
            var cmd = new UpdateProductTaxCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }


        [HttpDelete("delete/{taxId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid taxId)
        {
            var cmd = new DeleteProductTaxCommand(taxId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }


        [HttpDelete("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var cmd = new DeleteProductTaxesRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var cmd = new DeleteAllProductTaxesCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion

        #region ──────────── Query Endpoints ────────────
        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllProductTaxesQuery();
            var r = await _mediator.Send(q);
            return StatusCode((int)r.StatusCode, r);
        }
        [HttpGet("{taxId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid taxId)
        {
            var q = new GetProductTaxByIdQuery(taxId);
            var r = await _mediator.Send(q);
            return StatusCode((int)r.StatusCode, r);
        }
        [HttpGet("by-product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByProduct(Guid productId)
        {
            var q = new GetProductTaxesByProductQuery(productId);
            var r = await _mediator.Send(q);
            return StatusCode((int)r.StatusCode, r);
        }
        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetProductTaxesByIdsQuery(ids);
            var r = await _mediator.Send(q);
            return StatusCode((int)r.StatusCode, r);
        }
        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetProductTaxesCountQuery();
            var r = await _mediator.Send(q);
            return StatusCode((int)r.StatusCode, r);
        }
        [HttpGet("count/by-product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByProduct(Guid productId)
        {
            var q = new GetProductTaxesCountByProductQuery(productId);
            var r = await _mediator.Send(q);
            return StatusCode((int)r.StatusCode, r);
        }
        [HttpGet("with-product/{taxId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithProduct(Guid taxId)
        {
            var q = new GetProductTaxWithProductQuery(taxId);
            var r = await _mediator.Send(q);
            return StatusCode((int)r.StatusCode, r);
        }
        #endregion
    }
}
