using MediatR;  
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.Commands
{
    public class BulkRestoreProductsCommand : IRequest<APIResponseDto>
    {
        public ProductBulkRestoreDto Dto { get; }
        public BulkRestoreProductsCommand(ProductBulkRestoreDto dto) => Dto = dto;
    }
}
