using MediatR; 
namespace ErpSwiftCore.Application.Features.Products.ProductPrices.Queries
{
    public class GetPricesByCurrencyQuery : IRequest<APIResponseDto>
    {
        public Guid CurrencyId { get; }
        public GetPricesByCurrencyQuery(Guid currencyId) => CurrencyId = currencyId;
    }


}
