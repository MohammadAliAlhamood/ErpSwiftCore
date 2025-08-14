using ErpSwiftCore.Application.Features.Products.Product.Dtos;
using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Commands
{

    public class BulkDeleteProductsCommand : IRequest<APIResponseDto>
    {
        public ProductBulkDeleteDto Dto { get; }
        public BulkDeleteProductsCommand(ProductBulkDeleteDto dto) => Dto = dto;
    }
}
