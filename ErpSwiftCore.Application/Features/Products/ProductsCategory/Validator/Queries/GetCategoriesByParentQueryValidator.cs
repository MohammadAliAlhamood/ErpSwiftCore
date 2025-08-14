using ErpSwiftCore.Application.Features.Products.ProductsCategory.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductsCategory.Validator.Queries
{

    public class GetCategoriesByParentQueryValidator
        : SimpleIdQueryValidator<GetCategoriesByParentQuery>
    {
        public GetCategoriesByParentQueryValidator()
            : base(x => x.ParentCategoryId) { }
    }
}
