using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;

namespace ErpSwiftCore.Web.IService
{
    public interface IBaseService
    {
        Task<APIResponseDto?> SendAsync(
            APIRequestDto requestDto, 
            bool withBearer = true, 
            CancellationToken cancellationToken = default);
        Task<T> SendAsync<T>(APIRequestDto aPIRequestDto);
    }
}
