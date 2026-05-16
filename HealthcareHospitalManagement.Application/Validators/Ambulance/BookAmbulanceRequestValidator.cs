using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Ambulance;


namespace HealthcareHospitalManagement.Application.Validators.Ambulance;

public class BookAmbulanceRequestValidator : AbstractValidator<BookAmbulanceRequestDto>
{
    public BookAmbulanceRequestValidator()
    {
        RuleFor(x => x.VehicleId)
            .NotEmpty().WithMessage("VehicleId is required.")
            .NotEqual(Guid.Empty).WithMessage("VehicleId must not be an empty GUID.");

        RuleFor(x => x.ProviderId)
            .NotEmpty().WithMessage("ProviderId is required.")
            .NotEqual(Guid.Empty).WithMessage("ProviderId must not be an empty GUID.");

        RuleFor(x => x.RequesterName)
            .NotEmpty().WithMessage("Requester name is required.")
            .Length(2, 100).WithMessage("Requester name must be between 2 and 100 characters.");

        RuleFor(x => x.RequesterPhone)
            .NotEmpty().WithMessage("Requester phone is required.")
            .Matches(@"^\+?[0-9]{7,15}$").WithMessage("Requester phone must be a valid phone number.");

        RuleFor(x => x.PickupAddress)
            .NotEmpty().WithMessage("Pickup address is required.")
            .MaximumLength(500).WithMessage("Pickup address must not exceed 500 characters.");

        RuleFor(x => x.PickupLatitude)
            .InclusiveBetween(-90, 90).WithMessage("Pickup latitude must be between -90 and 90.")
            .When(x => x.PickupLatitude.HasValue);

        RuleFor(x => x.PickupLongitude)
            .InclusiveBetween(-180, 180).WithMessage("Pickup longitude must be between -180 and 180.")
            .When(x => x.PickupLongitude.HasValue);

        RuleFor(x => x.EmergencyDescription)
            .MaximumLength(1000).WithMessage("Emergency description must not exceed 1000 characters.")
            .When(x => x.EmergencyDescription != null);
    }
}
