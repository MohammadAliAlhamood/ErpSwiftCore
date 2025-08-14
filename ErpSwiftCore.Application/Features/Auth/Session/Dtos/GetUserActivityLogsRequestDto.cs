namespace ErpSwiftCore.Application.Features.Auth.Session.Dtos
{ 
    public class GetUserActivityLogsRequestDto
    {
        public string UserId { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
