using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceTaxDiscountModels;
using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;
namespace ErpSwiftCore.Web.Controllers.CompanyControllers.BillingsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class InvoiceTaxDiscountController : Controller
    {
        private readonly IInvoiceTaxDiscountService _svc;

        public InvoiceTaxDiscountController(IInvoiceTaxDiscountService svc)
        {
            _svc = svc ?? throw new ArgumentNullException(nameof(svc));
        }

      
    }
}
