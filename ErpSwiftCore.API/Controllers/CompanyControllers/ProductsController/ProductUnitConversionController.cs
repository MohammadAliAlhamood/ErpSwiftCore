using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ErpSwiftCore.API.Controllers.CompanyControllers.ProductsController
{
    [Route("api/product-unit-conversion")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class ProductUnitConversionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductUnitConversionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region ──────────── Command Endpoints ────────────
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] ProductUnitConversionCreateDto dto)
        {
            var cmd = new CreateProductUnitConversionCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] ProductUnitConversionUpdateDto dto)
        {
            var cmd = new UpdateProductUnitConversionCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpDelete("delete/{conversionId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid conversionId)
        {
            var cmd = new DeleteProductUnitConversionCommand(conversionId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpDelete("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var cmd = new DeleteProductUnitConversionsRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var cmd = new DeleteAllProductUnitConversionsCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        #endregion
        #region ──────────── Query Endpoints ────────────
        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllProductUnitConversionsQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("{conversionId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid conversionId)
        {
            var q = new GetProductUnitConversionByIdQuery(conversionId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByProduct(Guid productId)
        {
            var q = new GetProductUnitConversionsByProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("from-unit/{fromUnitId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByFromUnit(Guid fromUnitId)
        {
            var q = new GetProductUnitConversionsByFromUnitQuery(fromUnitId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("to-unit/{toUnitId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByToUnit(Guid toUnitId)
        {
            var q = new GetProductUnitConversionsByToUnitQuery(toUnitId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetProductUnitConversionsByIdsQuery(ids);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("with-product/{conversionId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithProduct(Guid conversionId)
        {
            var q = new GetProductUnitConversionWithProductQuery(conversionId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("with-from-unit/{conversionId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithFromUnit(Guid conversionId)
        {
            var q = new GetProductUnitConversionWithFromUnitQuery(conversionId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("with-to-unit/{conversionId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithToUnit(Guid conversionId)
        {
            var q = new GetProductUnitConversionWithToUnitQuery(conversionId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetProductUnitConversionsCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("count/product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByProduct(Guid productId)
        {
            var q = new GetProductUnitConversionsCountByProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        } 
        #endregion
    }
}
