using ErpSwiftCore.Application.Features.Auth.Session.Commands;
using ErpSwiftCore.Application.Features.Auth.Session.Validator.Dtos;
using FluentValidation; 
namespace ErpSwiftCore.Application.Features.Auth.Session.Validator.Queries
{
    /// <summary>
    /// Validator لـ GetUserActivityLogsQuery: يتضمّن GetUserActivityLogsRequestDto.
    /// </summary>
    public class GetUserActivityLogsQueryValidator : AbstractValidator<GetUserActivityLogsQuery>
    {
        public GetUserActivityLogsQueryValidator()
        {
            RuleFor(x => x.GetUserActivityLogsRequest)
                .NotNull()
                .WithMessage("بيانات استعلام سجلات النشاط مطلوبة.")
                .SetValidator(new GetUserActivityLogsRequestDtoValidator());
        }
    }
}
