namespace ErpSwiftCore.Application.Features.Auth.Session.Dtos
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
