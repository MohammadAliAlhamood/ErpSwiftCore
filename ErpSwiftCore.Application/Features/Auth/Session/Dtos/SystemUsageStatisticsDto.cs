namespace ErpSwiftCore.Application.Features.Auth.Session.Dtos
{ 
    public class SystemUsageStatisticsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int BlockedUsers { get; set; }
        public int TotalRoles { get; set; }
    }
}
