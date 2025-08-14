using ErpSwiftCore.Web.IService;
using ErpSwiftCore.Web.IService.IAuthsService;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using static ErpSwiftCore.Web.Utility.SD;

namespace ErpSwiftCore.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        }
        public async Task<APIResponseDto?> SendAsync(APIRequestDto requestDto, bool withBearer = true, CancellationToken cancellationToken = default)
        {
            var responseDto = new APIResponseDto();
            try
            {
                using var client = _httpClientFactory.CreateClient("ErpAPI");
                using var message = new HttpRequestMessage
                {
                    RequestUri = new Uri(requestDto.Url),
                    Method = GetHttpMethod(requestDto.ApiType)
                };

                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(requestDto.ContentType == ContentType.MultipartFormData ? "*/*" : "application/json"));

                if (withBearer)
                {
                    await AttachBearerTokenAsync(message);
                }

                if (requestDto.Data != null)
                {
                    message.Content = requestDto.ContentType == ContentType.MultipartFormData
                        ? CreateMultipartContent(requestDto.Data)
                        : new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                using var apiResponse = await client.SendAsync(message, cancellationToken);
                return await HandleApiResponse(apiResponse);
            }
            catch (Exception ex)
            {
                return new APIResponseDto
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessages = new List<string> { ex.Message }
                };
            }
        }

        /// <summary>
        /// يُحدد نوع الطلب HTTP بناءً على نوع العملية API.
        /// </summary>
        private static HttpMethod GetHttpMethod(ApiType apiType) =>
            apiType switch
            {
                ApiType.POST => HttpMethod.Post,
                ApiType.PUT => HttpMethod.Put,
                ApiType.PATCH => HttpMethod.Patch,
                ApiType.DELETE => HttpMethod.Delete,
                _ => HttpMethod.Get
            };

        /// <summary>
        /// يُضيف رمز المصادقة إلى الطلب في حالة تمكين Bearer Authentication.
        /// </summary>
        private async Task AttachBearerTokenAsync(HttpRequestMessage message)
        {
            var token = _tokenProvider.GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        /// <summary>
        /// يُنشئ محتوى الطلب عند التعامل مع البيانات متعددة الأجزاء (Multipart Form Data).
        /// </summary>
        private static MultipartFormDataContent CreateMultipartContent(object data)
        {
            var content = new MultipartFormDataContent();
            foreach (var prop in data.GetType().GetProperties())
            {
                var value = prop.GetValue(data);
                if (value is IFormFile file)
                {
                    content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                }
                else
                {
                    content.Add(new StringContent(value?.ToString() ?? ""), prop.Name);
                }
            }
            return content;
        }

        /// <summary>
        /// يُعالج استجابة API ويُعيد كائن APIResponseDto بناءً على النتيجة.
        /// </summary>


        private static async Task<APIResponseDto?> HandleApiResponse(HttpResponseMessage apiResponse)
        {
            APIResponseDto? responseDto = new APIResponseDto
            {
                StatusCode = apiResponse.StatusCode,
                IsSuccess = apiResponse.IsSuccessStatusCode
            };

            if (!apiResponse.IsSuccessStatusCode)
            {
                responseDto.ErrorMessages = new List<string> { apiResponse.ReasonPhrase ?? "Unknown Error" };
                responseDto.ErrorMessages.Add(apiResponse.StatusCode switch
                {
                    HttpStatusCode.NotFound => "Resource not found.",
                    HttpStatusCode.Forbidden => "Access denied.",
                    HttpStatusCode.Unauthorized => "Unauthorized request.",
                    HttpStatusCode.InternalServerError => "Internal server error.",
                    _ => "An error occurred."
                });

                return responseDto;
            }

            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            responseDto = JsonConvert.DeserializeObject<APIResponseDto>(apiContent);
            return responseDto;
        }

        public async Task<T> SendAsync<T>(APIRequestDto requestDto)
        {
            // Send the request and get the unified APIResponseDto
            var apiResponse = await SendAsync(requestDto, withBearer: true);

            if (apiResponse == null)
                throw new HttpRequestException("No response received from the API.");

            if (!apiResponse.IsSuccess)
            {
                // You can customize exception type or include more details
                var errors = apiResponse.ErrorMessages != null
                    ? string.Join("; ", apiResponse.ErrorMessages)
                    : apiResponse.StatusCode.ToString();
                throw new InvalidOperationException($"API request failed ({apiResponse.StatusCode}): {errors}");
            }

            // If the APIResponseDto wraps the actual payload in a `Data` property, deserialize it
            // We serialize it back to JSON first to ensure proper conversion
            var payloadJson = JsonConvert.SerializeObject(apiResponse.Result);
            return JsonConvert.DeserializeObject<T>(payloadJson)!;
        }

    }
}
