using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.DoctorDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.DoctorDocument
{
    public class UpdateDoctorDocumentValidator : AbstractValidator<UpdateDoctorDocumentDto>
    {
        private static readonly string[] AllowedFileTypes = ["pdf", "jpg", "jpeg", "png"];

        public UpdateDoctorDocumentValidator()
        {
            RuleFor(x => x.DocumentType)
                .IsInEnum().WithMessage("Invalid document type.");

            RuleFor(x => x.DocumentName)
                .NotEmpty().WithMessage("Document name is required.")
                .MaximumLength(200).WithMessage("Document name must not exceed 200 characters.");

            RuleFor(x => x.FileUrl)
                .NotEmpty().WithMessage("File URL is required.")
                .MaximumLength(500).WithMessage("File URL must not exceed 500 characters.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("File URL must be a valid URL.");

            RuleFor(x => x.FileType)
                .Must(ft => AllowedFileTypes.Contains(ft!.ToLower()))
                .WithMessage($"Allowed file types: {string.Join(", ", AllowedFileTypes)}.")
                .When(x => x.FileType != null);

            RuleFor(x => x.FileSizeBytes)
                .GreaterThan(0).WithMessage("File size must be greater than 0.")
                .LessThanOrEqualTo(10 * 1024 * 1024).WithMessage("File size must not exceed 10 MB.")
                .When(x => x.FileSizeBytes.HasValue);

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.Today).WithMessage("Expiry date must be in the future.")
                .When(x => x.ExpiryDate.HasValue);

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.")
                .When(x => x.Notes != null);
        }
    }

}
