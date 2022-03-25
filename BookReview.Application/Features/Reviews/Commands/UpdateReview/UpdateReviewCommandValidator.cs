using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("Id must be provided")
                .GreaterThan(0).WithMessage("A valid Id must be provided");
            RuleFor(r => r.Text)
                .NotEmpty().WithMessage("Valid text must be provided")
                .MinimumLength(10).WithMessage("Please, keep writing your review");
            RuleFor(r => r.State)
                .Must(x => x >= 0).WithMessage("Valid state must be provided");
        }
    }
}
