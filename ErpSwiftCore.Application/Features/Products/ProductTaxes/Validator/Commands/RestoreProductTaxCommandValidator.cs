using ErpSwiftCore.Application.Features.Products.ProductTaxes.Commands;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Products.ProductTaxes.Validator.Commands
{
    public class RestoreProductTaxCommandValidator
       : SingleTaxIdCommandValidator<RestoreProductTaxCommand>
    {
        public RestoreProductTaxCommandValidator(IProductTaxValidationService svc)
            : base(svc, cmd => cmd.TaxId) { }
    }

}
