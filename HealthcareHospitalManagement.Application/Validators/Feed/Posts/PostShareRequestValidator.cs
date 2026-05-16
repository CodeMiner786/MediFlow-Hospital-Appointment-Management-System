using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Feed.Posts
{
    public class PostShareRequestValidator : AbstractValidator<PostShareRequestDto>
    {
        public PostShareRequestValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("PostId is required.")
                .NotEqual(Guid.Empty).WithMessage("PostId must not be an empty GUID.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId must not be an empty GUID.");

            RuleFor(x => x.ShareNote)
                .MinimumLength(2).When(x => !string.IsNullOrEmpty(x.ShareNote))
                .WithMessage("ShareNote must be at least 2 characters long.")
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.ShareNote))
                .WithMessage("ShareNote must not exceed 500 characters.");
        }
    }
}
