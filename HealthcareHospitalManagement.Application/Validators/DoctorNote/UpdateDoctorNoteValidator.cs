using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorNote
{
    public class UpdateDoctorNoteValidator : AbstractValidator<UpdateDoctorNoteDto>
    {
        public UpdateDoctorNoteValidator()
        {
            RuleFor(x => x.NoteTitle)
                .NotEmpty().WithMessage("Note title is required.")
                .MaximumLength(200).WithMessage("Note title must not exceed 200 characters.");

            RuleFor(x => x.NoteContent)
                .NotEmpty().WithMessage("Note content is required.")
                .MaximumLength(5000).WithMessage("Note content must not exceed 5000 characters.");

            RuleFor(x => x.Tags)
                .MaximumLength(300).WithMessage("Tags must not exceed 300 characters.")
                .When(x => x.Tags != null);
        }
    }

}
