using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;


namespace HealthcareHospitalManagement.Application.Validators.Ambulance;

public class UpdateAmbulanceBookingStatusRequestValidator : AbstractValidator<UpdateAmbulanceBookingStatusRequestDto>
{
    public UpdateAmbulanceBookingStatusRequestValidator()
    {
        RuleFor(x => x.BookingId)
            .NotEmpty().WithMessage("BookingId is required.")
            .NotEqual(Guid.Empty).WithMessage("BookingId must not be an empty GUID.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid ambulance booking status.");

        RuleFor(x => x.Fare)
            .GreaterThan(0).WithMessage("Fare must be greater than 0.")
            .When(x => x.Fare.HasValue);
    }
}
