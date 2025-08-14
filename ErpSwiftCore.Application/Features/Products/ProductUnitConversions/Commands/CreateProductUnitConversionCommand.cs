using MediatR; 
using ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Dtos;
namespace ErpSwiftCore.Application.Features.Products.ProductUnitConversions.Commands
{
    public class CreateProductUnitConversionCommand : IRequest<APIResponseDto>
    {
        public ProductUnitConversionCreateDto Dto { get; }

        public CreateProductUnitConversionCommand(ProductUnitConversionCreateDto dto)
        {
            Dto = dto;
        }
    }
}