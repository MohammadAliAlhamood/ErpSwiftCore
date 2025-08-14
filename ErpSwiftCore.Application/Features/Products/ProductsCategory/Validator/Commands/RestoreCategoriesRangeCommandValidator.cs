using ErpSwiftCore.Application.Features.Products.ProductsCategory.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Commands
{
    public class RestoreCategoriesRangeCommandValidator
       : IdListCommandValidator<RestoreCategoriesRangeCommand>
    {
        public RestoreCategoriesRangeCommandValidator(IProductCategoryValidationService vs)
            : base(x => x.CategoryIds, vs)
        {
        }
    }

}
