using ErpSwiftCore.Application.Features.Auth.UserProfile.Queries;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Auth.UserProfile.Validator.Queries
{ 
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator() { }
    }
}
