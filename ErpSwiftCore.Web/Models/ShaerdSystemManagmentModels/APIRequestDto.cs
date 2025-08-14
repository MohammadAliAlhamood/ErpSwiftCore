using System.Net.Mime;
using System.Security.AccessControl;
using static ErpSwiftCore.Web.Utility.SD;
using ContentType = ErpSwiftCore.Web.Utility.SD.ContentType;

namespace ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels
{
    public class APIRequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
