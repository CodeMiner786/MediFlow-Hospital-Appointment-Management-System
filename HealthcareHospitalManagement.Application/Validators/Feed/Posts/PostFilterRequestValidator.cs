using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Feed.Posts
{
    public class PostFilterRequestValidator : AbstractValidator<PostFilterRequestDto>
    {
        public PostFilterRequestValidator()
        {
            RuleFor(x => x.PostType)
                .IsInEnum().When(x => x.PostType.HasValue)
                .WithMessage("Invalid PostType value.");

            RuleFor(x => x.Visibility)
                .IsInEnum().When(x => x.Visibility.HasValue)
                .WithMessage("Invalid Visibility value.");

            RuleFor(x => x.Status)
                .IsInEnum().When(x => x.Status.HasValue)
                .WithMessage("Invalid Status value.");

            RuleFor(x => x.Tag)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Tag));

            RuleFor(x => x.Keyword)
                .MinimumLength(2).When(x => !string.IsNullOrEmpty(x.Keyword))
                .MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Keyword));

            RuleFor(x => x.AuthorId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("AuthorId must be null or a valid GUID.");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
