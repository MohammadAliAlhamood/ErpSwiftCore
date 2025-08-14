using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.ICompaniesService;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.UnitOfMeasurements;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using ErpSwiftCore.Web.Utility;

namespace ErpSwiftCore.Web.Service.CompaniesService
{
    public class UnitOfMeasurementService : IUnitOfMeasurementService
    {
        private readonly IBaseService _baseService;

        public UnitOfMeasurementService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<APIResponseDto?> CreateUnitOfMeasurementAsync(UnitOfMeasurementCreateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/unit-of-measurement/create"
            });
        }

        public async Task<APIResponseDto?> UpdateUnitOfMeasurementAsync(UnitOfMeasurementUpdateDto dto)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = SD.ErpAPIBase + "/api/unit-of-measurement/update"
            });
        }

        public async Task<APIResponseDto?> DeleteUnitOfMeasurementAsync(Guid unitId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + $"/api/unit-of-measurement/delete/{unitId}"
            });
        }

        public async Task<APIResponseDto?> DeleteUnitsOfMeasurementRangeAsync(IEnumerable<Guid> unitIds)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Data = unitIds,
                Url = SD.ErpAPIBase + "/api/unit-of-measurement/delete-range"
            });
        }

        public async Task<APIResponseDto?> DeleteAllUnitsOfMeasurementAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ErpAPIBase + "/api/unit-of-measurement/delete-all"
            });
        }

        public async Task<APIResponseDto?> GetUnitOfMeasurementByIdAsync(Guid unitId)
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + $"/api/unit-of-measurement/{unitId}"
            });
        }

        public async Task<APIResponseDto?> GetAllUnitsOfMeasurementAsync()
        {
            return await _baseService.SendAsync(new APIRequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ErpAPIBase + "/api/unit-of-measurement/all"
            }, true);
        }
    }
}
