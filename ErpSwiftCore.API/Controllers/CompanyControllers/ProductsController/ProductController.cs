using MediatR;
using ErpSwiftCore.Utility;
using Microsoft.AspNetCore.Mvc;
using ErpSwiftCore.Application;
using Microsoft.AspNetCore.Authorization;
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using ErpSwiftCore.Application.Features.Products.Product.Queries;
using ErpSwiftCore.Application.Features.Products.Product.Commands;


namespace ErpSwiftCore.API.Controllers.CompanyControllers.ProductsController
{
    [Route("api/product")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        #region ──────────── Command Endpoints ────────────

        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create(ProductCreateDto dto)
        {
            var cmd = new CreateProductCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] ProductUpdateDto dto)
        {
            var cmd = new UpdateProductCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<APIResponseDto>> Delete([FromBody] ProductDeleteDto dto)
        {
            var cmd = new DeleteProductCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] ProductDeleteRangeDto dto)
        {
            var cmd = new DeleteProductsRangeCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

             #endregion

        #region ──────────── Query Endpoints ────────────




        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllProductsQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var res = await _mediator.Send(query);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-category/{categoryId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByCategory(Guid categoryId)
        {
            var q = new GetProductsByCategoryQuery(categoryId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-bundle/{bundleId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByBundle(Guid bundleId)
        {
            var q = new GetProductsByBundleQuery(bundleId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }


        [HttpGet("by-tax/{taxId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByTax(Guid taxId)
        {
            var q = new GetProductsByTaxQuery(taxId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-unit/{unitId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByUnit(Guid unitId)
        {
            var q = new GetProductsByUnitQuery(unitId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-price-type/{priceType}")]
        public async Task<ActionResult<APIResponseDto>> GetByPriceType(string priceType)
        {
            var q = new GetProductsByPriceTypeQuery(priceType);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-product-type/{productTypeId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByProductType(Guid productTypeId)
        {
            var q = new GetProductsByProductTypeQuery(productTypeId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

       

        [HttpGet("with-bundles/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithBundles(Guid id)
        {
            var q = new GetProductWithBundlesQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-prices/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithPrices(Guid id)
        {
            var q = new GetProductWithPricesQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-taxes/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithTaxes(Guid id)
        {
            var q = new GetProductWithTaxesQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-units/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithUnits(Guid id)
        {
            var q = new GetProductWithUnitsQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }


        #endregion
    }
}
