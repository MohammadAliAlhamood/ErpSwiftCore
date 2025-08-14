using MediatR; 
using ErpSwiftCore.Application.Features.Products.Product.Dtos;
namespace ErpSwiftCore.Application.Features.Products.Product.Commands
{
    public class BulkImportProductsCommand : IRequest<APIResponseDto>
    {
        public ProductBulkImportDto Dto { get; }
        public BulkImportProductsCommand(ProductBulkImportDto dto) => Dto = dto;
    }

}
