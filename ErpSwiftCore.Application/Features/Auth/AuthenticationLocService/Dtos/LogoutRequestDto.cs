namespace ErpSwiftCore.Application.Features.Auth.AuthenticationLocService.Dtos
{
    public class LogoutRequestDto
    {
        public string UserId { get; set; } = default!;
    } 
    public class LogoutResponseDto
    {
        public string Message { get; set; } 
            = "تم تسجيل الخروج بنجاح.";
    }

    public class LogoutAllSessionsResponseDto
    {
        public string Message { get; set; }
            = "تم تسجيل الخروج من جميع الجلسات بنجاح.";
    }
}