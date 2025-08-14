using ErpSwiftCore.Application.Features.Products.Product.Queries;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Products.Product.Validator.Queries
{

    public class GetProductByCodeQueryValidator : 
        AbstractValidator<GetProductByCodeQuery>
    {
        public GetProductByCodeQueryValidator()
        {
            RuleFor(q => q.Code)
                .NotEmpty().WithMessage("Code is required.");
        }
    } 
}
