using System.Net;

namespace ErpSwiftCore.Application
{
    public class APIResponseDto
    {
        public APIResponseDto()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object? Result { get; set; }
    }
}