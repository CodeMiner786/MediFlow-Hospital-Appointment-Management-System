using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorDocument
{
    public class VerifyDoctorDocumentValidator : AbstractValidator<VerifyDoctorDocumentDto>
    {
        public VerifyDoctorDocumentValidator()
        {
            RuleFor(x => x.VerifiedBy)
                .NotEmpty().WithMessage("VerifiedBy is required.")
                .MaximumLength(100).WithMessage("VerifiedBy must not exceed 100 characters.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
                .When(x => x.Notes != null);
        }
    }

}
