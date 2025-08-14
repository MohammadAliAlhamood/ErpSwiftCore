namespace ErpSwiftCore.Web.Models.AuthenticationSystemManagmentModels.UserProfile
{
    /// <summary>
    /// معلومات مطلوبة لتحديث الملف الشخصي للمستخدم الحالي.
    /// </summary>
    public class UpdateMyProfileRequestDto
    {
        public string Name { get; set; } = default!;
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Address { get; set; }
    }
}
