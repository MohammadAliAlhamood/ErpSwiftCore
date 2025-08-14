namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.ActivityLogs
{ 
    public class SessionDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
        public string? IpAddress { get; set; }
        public string? DeviceInfo { get; set; }
    }
}
