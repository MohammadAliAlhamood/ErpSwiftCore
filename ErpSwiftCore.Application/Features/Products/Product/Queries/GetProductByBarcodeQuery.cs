using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductByBarcodeQuery : IRequest<APIResponseDto>
    {
        public string Barcode { get; }
        public GetProductByBarcodeQuery(string barcode) => Barcode = barcode;
    }

}
