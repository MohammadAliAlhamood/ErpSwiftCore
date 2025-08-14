namespace ErpSwiftCore.Application.Features.Auth.Session.Dtos
{ 
    public class ActivityLogDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public DateTimeOffset Timestamp { get; set; }

        public string ActivityType { get; set; } = string.Empty;

        // عنوان الـ IP الذي تمت منه العملية
        public string? IpAddress { get; set; }

        // معلومات عن الجهاز المستخدَم (User-Agent مثلاً)
        public string? DeviceInfo { get; set; }
    }

}
