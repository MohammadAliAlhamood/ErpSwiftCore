namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserProfile
{
    public class UserDto
    {
        public string Id { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Address { get; set; }
        public bool IsBlocked { get; set; }  // أضفها هنا

        /// <summary>
        /// مجموعة أسماء الأدوار (Roles) المرتبطة بالمستخدم.
        /// </summary>
        public IEnumerable<string>? Roles { get; set; }
    }
}