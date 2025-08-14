using ErpSwiftCore.Application.Dtos;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Dtos;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using MediatR; 

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Queries
{
    public class GetProductTaxByIdQuery : IRequest<APIResponseDto>
    {
        public Guid TaxId { get; }

        public GetProductTaxByIdQuery(Guid taxId)
        {
            TaxId = taxId;
        }
    }
}