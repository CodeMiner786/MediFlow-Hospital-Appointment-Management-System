using FluentValidation;
using HealthcareHospitalManagement.Application.DTOs.Feed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareHospitalManagement.Application.Validators.Feed
{
    public class FeedItemSaveRequestValidator : AbstractValidator<FeedItemSaveRequestDto>
    {
        public FeedItemSaveRequestValidator()
        {
            RuleFor(x => x.FeedItemId)
                .NotEmpty().WithMessage("FeedItemId is required.")
                .NotEqual(Guid.Empty).WithMessage("FeedItemId must not be an empty GUID.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId must not be an empty GUID.");
        }
    }
}
