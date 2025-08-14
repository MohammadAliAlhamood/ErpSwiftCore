using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSwiftCore.Web.Helpers
{
    public interface ISelectListService
    {
        Task<IEnumerable<SelectListItem>> GetProductSelectListAsync(Guid? selectedId = null);
        Task<IEnumerable<SelectListItem>> GetCurrencySelectListAsync(Guid? selectedId = null);
        Task<IEnumerable<SelectListItem>> GetUnitSelectListAsync(Guid? selectedId = null);
        Task<IEnumerable<SelectListItem>> GetCategorySelectListAsync(Guid? selectedId = null);
        Task<IEnumerable<SelectListItem>> GetWarehouseSelectListAsync(Guid? warehouseID = null);
        Task<IEnumerable<SelectListItem>> GetBranchSelectListAsync(Guid? branchID = null);
        Task<IEnumerable<SelectListItem>> GetOrderSelectListAsync(Guid? orderId = null);
        Task<IEnumerable<SelectListItem>> GetParentAccountSelectListAsync(Guid? parentAccountId = null);
        Task<IEnumerable<SelectListItem>> GetCustomerSelectListAsync(Guid? selectedId = null);
        Task<IEnumerable<SelectListItem>> GetSupplierSelectListAsync(Guid? selectedId = null);

    }
}