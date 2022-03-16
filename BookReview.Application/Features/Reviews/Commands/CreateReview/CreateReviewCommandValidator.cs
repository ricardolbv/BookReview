using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(r => r.Text)
                .NotEmpty().WithMessage("Review must have a value")
                .NotNull()
                .MinimumLength(10).WithMessage("Please, keep writing your review");
        }
    }
}
