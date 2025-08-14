using ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands;
using ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Dtos;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Commands
{
    public class CreateProductTaxCommandValidator : AbstractValidator<CreateProductTaxCommand>
    {
        public CreateProductTaxCommandValidator(IProductTaxValidationService svc)
        {
            RuleFor(x => x.Tax)
                .NotNull().WithMessage("Payload is required.")
                .SetValidator(new ProductTaxCreateDtoValidator(svc));
        }
    }

    public class BulkCreateProductTaxesCommandValidator : AbstractValidator<BulkCreateProductTaxesCommand>
    {
        public BulkCreateProductTaxesCommandValidator(IProductTaxValidationService svc)
        {
            RuleFor(x => x.Taxes)
                .NotNull().WithMessage("Taxes collection is required.")
                .NotEmpty().WithMessage("At least one tax must be provided.")
                .ForEach(t => t.SetValidator(new ProductTaxCreateDtoValidator(svc)));
        }
    }

    public class UpdateProductTaxCommandValidator : AbstractValidator<UpdateProductTaxCommand>
    {
        public UpdateProductTaxCommandValidator(IProductTaxValidationService svc)
        {
            RuleFor(x => x.Tax)
                .NotNull().WithMessage("Payload is required.")
                .SetValidator(new ProductTaxUpdateDtoValidator(svc));
        }
    }

    public abstract class BaseTaxIdsCommandValidator<T> : AbstractValidator<T> where T : class
    {
        protected BaseTaxIdsCommandValidator(IProductTaxValidationService svc, System.Func<T, IEnumerable<Guid>> selector)
        {
            RuleFor(x => selector(x))
                .NotNull().WithMessage("TaxIds collection is required.")
                .NotEmpty().WithMessage("At least one TaxId must be provided.")
                .MustAsync(async (ids, ct) =>
                {
                    foreach (var id in ids)
                        if (!await svc.TaxExistsByIdAsync(id, ct)) return false;
                    return true;
                })
                .WithMessage("One or more TaxIds do not exist.");
        }
    }

    public class SoftDeleteProductTaxesRangeCommandValidator
        : BaseTaxIdsCommandValidator<SoftDeleteProductTaxesRangeCommand>
    {
        public SoftDeleteProductTaxesRangeCommandValidator(IProductTaxValidationService svc)
            : base(svc, cmd => cmd.TaxIds) { }
    }

    public class DeleteProductTaxesRangeCommandValidator
        : BaseTaxIdsCommandValidator<DeleteProductTaxesRangeCommand>
    {
        public DeleteProductTaxesRangeCommandValidator(IProductTaxValidationService svc)
            : base(svc, cmd => cmd.TaxIds) { }
    }

    public class RestoreProductTaxesRangeCommandValidator
        : BaseTaxIdsCommandValidator<RestoreProductTaxesRangeCommand>
    {
        public RestoreProductTaxesRangeCommandValidator(IProductTaxValidationService svc)
            : base(svc, cmd => cmd.TaxIds) { }
    }

    public class BulkDeleteProductTaxesCommandValidator
        : BaseTaxIdsCommandValidator<BulkDeleteProductTaxesCommand>
    {
        public BulkDeleteProductTaxesCommandValidator(IProductTaxValidationService svc)
            : base(svc, cmd => cmd.TaxIds) { }
    }

    public class BulkSoftDeleteProductTaxesCommandValidator
        : BaseTaxIdsCommandValidator<BulkSoftDeleteProductTaxesCommand>
    {
        public BulkSoftDeleteProductTaxesCommandValidator(IProductTaxValidationService svc)
            : base(svc, cmd => cmd.TaxIds) { }
    }

    public class BulkRestoreProductTaxesCommandValidator
        : BaseTaxIdsCommandValidator<BulkRestoreProductTaxesCommand>
    {
        public BulkRestoreProductTaxesCommandValidator(IProductTaxValidationService svc)
            : base(svc, cmd => cmd.TaxIds) { }
    }

    

    
   
   
    

    
}