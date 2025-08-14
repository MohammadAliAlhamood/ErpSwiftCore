using FluentValidation; 
using ErpSwiftCore.Application.Features.Products.Product.Queries;
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{

    public class GetProductsByUnitQueryValidator 
                : AbstractValidator<GetProductsByUnitQuery>
    {
        public GetProductsByUnitQueryValidator()
        {
            RuleFor(q => q.UnitId)
                .NotEmpty().WithMessage("UnitId is required.");
        }
    }

}
