
using ErpSwiftCore.Application.Dtos.Person;

namespace ErpSwiftCore.Application.Features.CRMs.Suppliers.Dtos
{
    /// <summary>
    /// بيانات تحديث مورد موجود
    /// </summary>
    public class UpdateSupplierDto : UpdatePersonDto
    {
        public string SupplierCode { get; set; } = null!;
        public decimal? MaxSupplyLimit { get; set; }
    }
}
