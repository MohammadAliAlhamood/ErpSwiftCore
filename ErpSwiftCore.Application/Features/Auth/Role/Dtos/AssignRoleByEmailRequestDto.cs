namespace ErpSwiftCore.Application.Features.Auth.Role.Dtos
{
    /// <summary>
    /// بيانات الطلب لتعيين دور للمستخدم اعتمادًا على البريد الإلكتروني.
    /// </summary>
    public class AssignRoleByEmailRequestDto
    {
        /// <summary>
        /// البريد الإلكتروني للمستخدم.
        /// </summary>
        public string Email { get; set; } = default!;

        /// <summary>
        /// اسم الدور المراد تعيينه للمستخدم.
        /// </summary>
        public string RoleName { get; set; } = default!;
    }
}
