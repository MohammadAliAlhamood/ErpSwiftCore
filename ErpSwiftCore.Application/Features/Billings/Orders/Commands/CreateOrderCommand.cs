using ErpSwiftCore.Application.Features.Billings.Orders.Dtos;
using MediatR; 

namespace ErpSwiftCore.Application.Features.Billings.Orders.Commands
{
    /// <summary>
    /// 1. Create order (with optional lines)
    /// </summary>
    public class CreateOrderCommand : IRequest<APIResponseDto>
    {
        public CreateOrderDto Dto { get; }
        public CreateOrderCommand(CreateOrderDto dto) => Dto = dto;
    }

   
   


    
    


   

    
    
}