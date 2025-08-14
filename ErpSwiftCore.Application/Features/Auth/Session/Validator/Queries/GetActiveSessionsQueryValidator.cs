using ErpSwiftCore.Application.Features.Auth.Session.Commands;
using ErpSwiftCore.Application.Features.Auth.Session.Validator.Dtos; 
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Auth.Session.Validator.Queries
{
    /// <summary>
    /// Validator لـ GetActiveSessionsQuery: يتضمّن GetActiveSessionsRequestDto.
    /// </summary>
    public class GetActiveSessionsQueryValidator : AbstractValidator<GetActiveSessionsQuery>
    {
        public GetActiveSessionsQueryValidator()
        {
            RuleFor(x => x.GetActiveSessionsRequest)
                .NotNull()
                .WithMessage("بيانات استعلام الجلسات النشطة مطلوبة.")
                .SetValidator(new GetActiveSessionsRequestDtoValidator());
        }
    }
}
