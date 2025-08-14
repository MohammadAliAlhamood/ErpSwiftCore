using ErpSwiftCore.Application; 
using ErpSwiftCore.Application.Features.Billings.Orders.Commands;
using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using ErpSwiftCore.Application.Features.Billings.Orders.Queries;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Utility;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
namespace ErpSwiftCore.API.Controllers.CompanyControllers.BillingsController
{
    [Route("api/order")]
    [ApiController]
    [Authorize(Roles = SD.Role_Company)]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries
        // 0. Get all orders with details
        [HttpGet("all-with-details")]
        public async Task<ActionResult<APIResponseDto>> GetAllWithDetails()
        {
            var q = new GetAllOrdersWithDetailsQuery();
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 0b. Get orders filtered by status
        [HttpGet("by-status/{status}")]
        public async Task<ActionResult<APIResponseDto>> GetByStatus(OrderStatus status)
        {
            var q = new GetOrdersByStatusQuery(status);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }
        // 1. Get order by ID
        [HttpGet("{orderId:guid}")]
        public async Task<ActionResult<APIResponseDto>> GetById(Guid orderId)
        {
            var q = new GetOrderByIdQuery(orderId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 2. Get orders by IDs
        [HttpPost("by-ids")]
        public async Task<ActionResult<APIResponseDto>> GetByIds([FromBody] IEnumerable<Guid> orderIds)
        {
            var q = new GetOrdersByIdsQuery(orderIds);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        

        // 4. Get order lines for an order
        [HttpGet("{orderId:guid}/lines")]
        public async Task<ActionResult<APIResponseDto>> GetLines(Guid orderId)
        {
            var q = new GetOrderLinesQuery(orderId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 5. Get order lines count
        [HttpGet("{orderId:guid}/lines/count")]
        public async Task<ActionResult<APIResponseDto>> GetLinesCount(Guid orderId)
        {
            var q = new GetOrderLinesCountQuery(orderId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 6. Validate entire order
        [HttpGet("validate/{orderId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Validate(Guid orderId)
        {
            var q = new ValidateOrderQuery(orderId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 7. Check if order exists
        [HttpGet("validate/exists/{orderId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Exists(Guid orderId)
        {
            var q = new OrderExistsQuery(orderId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 8. Check if a specific order line exists
        [HttpGet("validate/line-exists/{orderLineId:guid}")]
        public async Task<ActionResult<APIResponseDto>> LineExists(Guid orderLineId)
        {
            var q = new OrderLineExistsQuery(orderLineId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 9. Check if an order has any lines
        [HttpGet("validate/has-lines/{orderId:guid}")]
        public async Task<ActionResult<APIResponseDto>> HasLines(Guid orderId)
        {
            var q = new HasOrderLinesQuery(orderId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 10. Check if order is linked to a specific party
        [HttpGet("validate/linked-party/{orderId:guid}/{partyId:guid}")]
        public async Task<ActionResult<APIResponseDto>> IsLinked(Guid orderId, Guid partyId)
        {
            var q = new IsOrderLinkedToPartyQuery(orderId, partyId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        // 11. Calculate total amount of an order
        [HttpGet("{orderId:guid}/total")]
        public async Task<ActionResult<APIResponseDto>> CalculateTotal(Guid orderId)
        {
            var q = new CalculateOrderTotalQuery(orderId);
            var res = await _mediator.Send(q);
            return StatusCode((int)res.StatusCode, res);
        }

        #endregion
        #region Commands

        // 1. Create order (with optional lines)
        [HttpPost("create")]
        public async Task<ActionResult<APIResponseDto>> Create([FromBody] CreateOrderDto dto)
        {
            var cmd = new CreateOrderCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 2. Update order (manage lines)
        [HttpPut("update")]
        public async Task<ActionResult<APIResponseDto>> Update([FromBody] UpdateOrderDto dto)
        {
            var cmd = new UpdateOrderCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 3. Delete order
        [HttpDelete("delete/{orderId:guid}")]
        public async Task<ActionResult<APIResponseDto>> Delete(Guid orderId)
        {
            var cmd = new DeleteOrderCommand(orderId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 4. Add order line
        [HttpPost("{orderId:guid}/lines/add")]
        public async Task<ActionResult<APIResponseDto>> AddLine(Guid orderId, [FromBody] CreateOrderLineDto dto)
        {
            var cmd = new AddOrderLineCommand(orderId, dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 5. Add multiple order lines
        [HttpPost("{orderId:guid}/lines/add-multiple")]
        public async Task<ActionResult<APIResponseDto>> AddLines(Guid orderId, [FromBody] IEnumerable<CreateOrderLineDto> dtos)
        {
            var cmd = new AddOrderLinesCommand(orderId, dtos);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 6. Update order line
        [HttpPut("lines/update")]
        public async Task<ActionResult<APIResponseDto>> UpdateLine([FromBody] UpdateOrderLineDto dto)
        {
            var cmd = new UpdateOrderLineCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 7. Delete order line
        [HttpDelete("lines/delete/{orderLineId:guid}")]
        public async Task<ActionResult<APIResponseDto>> DeleteLine(Guid orderLineId)
        {
             var cmd = new DeleteOrderLineCommand(orderLineId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 8. Delete all lines of an order
        [HttpDelete("{orderId:guid}/lines/delete-all")]
        public async Task<ActionResult<APIResponseDto>> DeleteAllLines(Guid orderId)
        { 
            var cmd = new DeleteAllOrderLinesCommand(orderId);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 9. Change order status
        [HttpPut("status/change")]
        public async Task<ActionResult<APIResponseDto>> ChangeStatus([FromBody] ChangeOrderStatusDto dto)
        {
            var cmd = new ChangeOrderStatusCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        // 10. Bulk delete orders
        [HttpPost("delete-range")]
        public async Task<ActionResult<APIResponseDto>> BulkDelete([FromBody] IEnumerable<Guid> OrderIds)
        {
            var cmd = new BulkDeleteOrdersCommand(OrderIds);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("create-with-lines")]
        public async Task<ActionResult<APIResponseDto>> CreateWithLines([FromBody] CreateOrderWithLinesDto dto)
        {
            var cmd = new CreateOrderWithLinesCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }
        [HttpPut("update-with-lines")]
        public async Task<ActionResult<APIResponseDto>> UpdateWithLines([FromBody] UpdateOrderWithLinesDto dto)
        {
            var cmd = new UpdateOrderWithLinesCommand(dto);
            var res = await _mediator.Send(cmd);
            return StatusCode((int)res.StatusCode, res);
        }


        #endregion










    }
}
