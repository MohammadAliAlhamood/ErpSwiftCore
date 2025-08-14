using MediatR;
using ErpSwiftCore.Utility;
using Microsoft.AspNetCore.Mvc; 
using ErpSwiftCore.Application;
using Microsoft.AspNetCore.Authorization;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Queries;
using ErpSwiftCore.Application.Features.Products.ProductPrices.Commands;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.ProductsController
{
    [Route("api/product-price")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class ProductPriceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductPriceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region ──────────── Command Endpoints ────────────
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] ProductPriceCreateDto dto)
        {
            var cmd = new CreateProductPriceCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] ProductPriceUpdateDto dto)
        {
            var cmd = new UpdateProductPriceCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpDelete("delete/{priceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid priceId)
        {
            var cmd = new DeleteProductPriceCommand(priceId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpDelete("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var cmd = new DeleteProductPricesRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var cmd = new DeleteAllProductPricesCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        #endregion

        #region ──────────── Query Endpoints ────────────
        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllPricesQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("{priceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid priceId)
        {
            var q = new GetPriceByIdQuery(priceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("latest/{productId:guid}/{priceType}")]
        public async Task<ActionResult<APIResponseDto>> GetLatest(Guid productId, string priceType)
        {
            var q = new GetLatestPriceByProductQuery(productId, priceType);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("by-product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByProduct(Guid productId)
        {
            var q = new GetPricesByProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("by-type/{priceType}")]
        public async Task<ActionResult<APIResponseDto>> GetByType(string priceType)
        {
            var q = new GetPricesByTypeQuery(priceType);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("by-currency/{currencyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByCurrency(Guid currencyId)
        {
            var q = new GetPricesByCurrencyQuery(currencyId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetPricesByIdsQuery(ids);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetPricesCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("count/by-product/{productId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByProduct(Guid productId)
        {
            var q = new GetPricesCountByProductQuery(productId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("count/by-type/{priceType}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByType(string priceType)
        {
            var q = new GetPricesCountByTypeQuery(priceType);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("with-product/{priceId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithProduct(Guid priceId)
        {
            var q = new GetPriceWithProductQuery(priceId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        #endregion
    }
}
