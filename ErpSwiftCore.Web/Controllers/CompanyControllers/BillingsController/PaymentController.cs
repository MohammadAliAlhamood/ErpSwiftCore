using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.PaymentModels;
using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.Utility; 
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.BillingsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _svc;

        public PaymentController(IPaymentService svc)
        {
            _svc = svc ?? throw new ArgumentNullException(nameof(svc));
        }

    }
}
