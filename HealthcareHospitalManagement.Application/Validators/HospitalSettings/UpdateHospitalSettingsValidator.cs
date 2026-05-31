using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.HospitalSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.HospitalSettings
{
    public class UpdateHospitalSettingsValidator : AbstractValidator<UpdateHospitalSettingsDto>
    {
        public UpdateHospitalSettingsValidator()
        {
            RuleFor(x => x.DefaultPlatformSharePercent)
                .InclusiveBetween(1, 99).WithMessage("Platform share percent must be between 1 and 99.");

            RuleFor(x => x.DefaultDoctorSharePercent)
                .InclusiveBetween(1, 99).WithMessage("Doctor share percent must be between 1 and 99.");

            RuleFor(x => x)
                .Must(x => x.DefaultPlatformSharePercent + x.DefaultDoctorSharePercent == 100)
                .WithMessage("Platform share and Doctor share must add up to 100%.");
        }
    }

}
