namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Dtos
{
    /// <summary>
    /// طلب حظر مستخدم.
    /// </summary>
    public class BlockUserRequestDto
    {
        public string UserId { get; set; } = default!;
    }
}
