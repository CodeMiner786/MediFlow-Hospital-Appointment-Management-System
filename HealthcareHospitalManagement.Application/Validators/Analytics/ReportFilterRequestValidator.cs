using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Analytics;


namespace HealthcareHospitalManagement.Application.Validators.Analytics;

public class ReportFilterRequestValidator : AbstractValidator<ReportFilterRequestDto>
{
    public ReportFilterRequestValidator()
    {
        RuleFor(x => x.DateFrom)
            .NotEmpty().WithMessage("DateFrom is required.")
            .LessThanOrEqualTo(x => x.DateTo).WithMessage("DateFrom must be before or equal to DateTo.");

        RuleFor(x => x.DateTo)
            .NotEmpty().WithMessage("DateTo is required.")
            .GreaterThanOrEqualTo(x => x.DateFrom).WithMessage("DateTo must be after or equal to DateFrom.");

        RuleFor(x => x.DoctorId)
            .NotEqual(Guid.Empty).WithMessage("DoctorId must not be an empty GUID.")
            .When(x => x.DoctorId.HasValue);

        RuleFor(x => x.DepartmentId)
            .NotEqual(Guid.Empty).WithMessage("DepartmentId must not be an empty GUID.")
            .When(x => x.DepartmentId.HasValue);
    }
}
