using MediatR;
using ErpSwiftCore.Utility;
using ErpSwiftCore.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries;
using ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands;

namespace ErpSwiftCore.API.Controllers.CompanyControllers.ProductsController
{
    [Route("api/product-category")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
 
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] ProductCategoryCreateDto dto)
        {
            var cmd = new CreateCategoryCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] ProductCategoryUpdateDto dto)
        {
            var cmd = new UpdateCategoryCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpDelete("delete/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid id)
        {
            var cmd = new DeleteCategoryCommand(id);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete-range")]
        public async Task<ActionResult<APIResponseDto>> DeleteRange([FromBody] IEnumerable<Guid> ids)
        {
            var cmd = new DeleteCategoriesRangeCommand(ids);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpDelete("delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAll()
        {
            var cmd = new DeleteAllCategoriesCommand();
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
  
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid id)
        {
            GetCategoryByIdQuery q = new(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpGet("soft-deleted/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetSoftDeletedById(Guid id)
        {
            var q = new GetSoftDeletedCategoryByIdQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<APIResponseDto>> GetAll()
        {
            var q = new GetAllCategoriesQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("by-parent/{parentId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetByParent(Guid parentId)
        {
            var q = new GetCategoriesByParentQuery(parentId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> ids)
        {
            var q = new GetCategoriesByIdsQuery(ids);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("descendants/{parentId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetDescendants(Guid parentId)
        {
            var q = new GetAllDescendantCategoriesQuery(parentId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("hierarchy/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetHierarchy(Guid id)
        {
            var q = new GetCategoryHierarchyQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-parent/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithParent(Guid id)
        {
            var q = new GetCategoryWithParentQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("with-subcategories/{id:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetWithSubCategories(Guid id)
        {
            var q = new GetCategoryWithSubCategoriesQuery(id);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count")]
        public async Task<ActionResult<APIResponseDto>> GetCount()
        {
            var q = new GetCategoriesCountQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpGet("count/parent/{parentId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetCountByParent(Guid parentId, [FromQuery] bool recursive = false)
        {
            var q = new GetCategoriesCountByParentQuery(parentId, recursive);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
    }
}
