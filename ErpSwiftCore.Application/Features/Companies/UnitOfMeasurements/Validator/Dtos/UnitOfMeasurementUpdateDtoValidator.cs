using ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Dtos;
using ErpSwiftCore.Domain.IServices.ICompanyServices.IUnitOfMeasurementService;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.UnitOfMeasurements.Validator.Dtos
{
    public class UnitOfMeasurementUpdateDtoValidator : AbstractValidator<UnitOfMeasurementUpdateDto>
    {
        private readonly IUnitOfMeasurementQueryService _queryService;

        public UnitOfMeasurementUpdateDtoValidator(IUnitOfMeasurementQueryService queryService)
        {
            _queryService = queryService;

            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("معرّف وحدة القياس مطلوب.")
                .MustAsync(ExistsUnit).WithMessage("وحدة القياس غير موجودة.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم وحدة القياس مطلوب.")
                .MaximumLength(100).WithMessage("اسم وحدة القياس يجب ألا يتجاوز 100 حرف.");

            RuleFor(x => x.Abbreviation)
                .NotEmpty().WithMessage("الاختصار مطلوب.")
                .MaximumLength(10).WithMessage("الاختصار يجب ألا يتجاوز 10 أحرف.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("الوصف يجب ألا يتجاوز 250 حرف.");
        }

        private async Task<bool> ExistsUnit(Guid id, CancellationToken ct)
        {
            return await _queryService.ExistsUnitOfMeasurementAsync(id, ct);
        }
    }
}