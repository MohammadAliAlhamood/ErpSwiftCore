using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.Product.Queries
{

    public class GetProductByCodeQuery : IRequest<APIResponseDto>
    {
        public string Code { get; }
        public GetProductByCodeQuery(string code) => Code = code;
    }

}
