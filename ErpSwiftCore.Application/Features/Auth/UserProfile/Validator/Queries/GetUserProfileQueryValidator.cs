using ErpSwiftCore.Application.Features.Auth.UserProfile.Queries;
using FluentValidation;
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Queries
{
    public class GetUserProfileQueryValidator : AbstractValidator<GetUserProfileQuery>
    {
        public GetUserProfileQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("معرّف المستخدم (UserId) مطلوب لاستعلام الملف الشخصي.");
        }
    }
}
