using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ErpSwiftCore.Web.Enums;
using ErpSwiftCore.Web.Helpers;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels; 
using ErpSwiftCore.Web.IService.IBillingsService;
using ErpSwiftCore.Web.Utility;
using ErpSwiftCore.Web.Models.BillingSystemManagmentModels.InvoiceModels;
using ErpSwiftCore.Web.ViewModels;

namespace ErpSwiftCore.Web.Controllers.CompanyControllers.BillingsController
{
    [Authorize(Roles = SD.Role_Company)]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _svc;
        private readonly ISelectListService _selectList;

        public InvoiceController(IInvoiceService svc, ISelectListService selectList)
        {
            _svc = svc ?? throw new ArgumentNullException(nameof(svc));
            _selectList = selectList ?? throw new ArgumentNullException(nameof(selectList));
        }

           }
}
