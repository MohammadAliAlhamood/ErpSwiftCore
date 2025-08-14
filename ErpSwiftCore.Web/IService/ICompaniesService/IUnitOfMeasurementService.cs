using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using System.Threading.Tasks;

namespace ErpSwiftCore.Web.IService.ICompaniesService
{
    public interface IUnitOfMeasurementService
    {
        Task<APIResponseDto?> CreateUnitOfMeasurementAsync(UnitOfMeasurementCreateDto dto);
        Task<APIResponseDto?> UpdateUnitOfMeasurementAsync(UnitOfMeasurementUpdateDto dto);
        Task<APIResponseDto?> DeleteUnitOfMeasurementAsync(Guid unitId);
        Task<APIResponseDto?> DeleteUnitsOfMeasurementRangeAsync(IEnumerable<Guid> unitIds);
        Task<APIResponseDto?> DeleteAllUnitsOfMeasurementAsync();
        Task<APIResponseDto?> GetUnitOfMeasurementByIdAsync(Guid unitId);
        Task<APIResponseDto?> GetAllUnitsOfMeasurementAsync();
    }
}


 