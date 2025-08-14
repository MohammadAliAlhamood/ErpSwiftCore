using ErpSwiftCore.Application;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Commands;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductBundles.Queries;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ErpSwiftCore.API.Controllers.CompanyControllers.ProductsController
{
    [Route("api/product-bundle")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class ProductBundleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductBundleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region ──────────── Command Endpoints ────────────
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] ProductBundleCreateDto dto)
        {
            var command = new CreateProductBundleCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] ProductBundleUpdateDto dto)
        {
            var command = new UpdateProductBundleCommand(dto);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("delete/{bundleId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid bundleId)
        {
            var command = new DeleteProductBundleCommand(bundleId);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var command = new DeleteProductBundlesRangeCommand(ids);
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var command = new DeleteAllProductBundlesCommand();
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }
        #endregion

        #region ──────────── Query Endpoints ────────────
        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var query = new GetAllProductBundlesQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
         
        [HttpGet("{bundleId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid bundleId)
        {
            var query = new GetProductBundleByIdQuery(bundleId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("parent/{parentProductId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByParentProduct(Guid parentProductId)
        {
            var query = new GetProductBundlesByParentProductQuery(parentProductId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("component/{componentProductId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByComponentProduct(Guid componentProductId)
        {
            var query = new GetProductBundlesByComponentProductQuery(componentProductId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("unit/{unitOfMeasurementId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByUnit(Guid unitOfMeasurementId)
        {
            var query = new GetProductBundlesByUnitQuery(unitOfMeasurementId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var query = new GetProductBundlesByIdsQuery(ids);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }


        [HttpGet("with-parent/{bundleId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithParent(Guid bundleId)
        {
            var query = new GetProductBundleWithParentProductQuery(bundleId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("with-component/{bundleId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithComponent(Guid bundleId)
        {
            var query = new GetProductBundleWithComponentProductQuery(bundleId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("with-unit/{bundleId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithUnit(Guid bundleId)
        {
            var query = new GetProductBundleWithUnitQuery(bundleId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var query = new GetProductBundlesCountQuery();
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("count/parent/{parentProductId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByParent(Guid parentProductId)
        {
            var query = new GetProductBundlesCountByParentProductQuery(parentProductId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("count/component/{componentProductId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByComponent(Guid componentProductId)
        {
            var query = new GetProductBundlesCountByComponentProductQuery(componentProductId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
        #endregion
    }
}
