using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Feed.Posts
{
    public class PostSaveRequestValidator : AbstractValidator<PostSaveRequestDto>
    {
        public PostSaveRequestValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("PostId is required.")
                .NotEqual(Guid.Empty).WithMessage("PostId must not be an empty GUID.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId must not be an empty GUID.");
        }
    }
}
